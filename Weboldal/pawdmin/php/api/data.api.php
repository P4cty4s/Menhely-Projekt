<?php

    // import
    require_once "../../config.php";
    require_once BASEAPI;
    require_once PHP_UTIL;
    require_once DATABASE;

    class dataapi extends baseapi
    {
        public function __construct()
        {
            parent::__construct();
            $this->listen();
            $this->sendresponse();
        }

        private function listen()
        {
            $method = $_SERVER['REQUEST_METHOD'];

            switch($method)
            {
                case 'GET': $this->getRequests(); break;
                case 'POST': $this->postRequests(); break;
                case 'DELETE': $this->deleteRequests(); break;
                case 'PUT': $this->putRequests(); break;
            }

        }   

        /* put */
        private function putRequests()
        {
            parse_str(file_get_contents("php://input"), $_PUT);
            switch($_PUT['action'])
            {
                case 'modify-szervezet':
                    $this->modifySzervezet();
                    break;
                case 'change-udvarnev':
                    $this->changeUdvarnev();
                    break;
                case 'modify-user':
                    $this->modifyUser();
                    break;
                case 'modify-telephely':
                    $this->modifyTelephely();
                    break;
            }
        }

        private function modifyTelephely()
        {
            parse_str(file_get_contents("php://input"), $_PUT);

            $telephely = db::get('SELECT * FROM telephely WHERE id = ?',[$_PUT['id']]);
            db::insertChangelog('telephely módosítva',$telephely);

            $params = [$_PUT['nev'],$_PUT['hely'],$_PUT['email'],$_PUT['telefonszam'],$_PUT['id']];
            db::execute('UPDATE telephely SET nev = ?, hely = ?, email = ?, telefonszam = ? WHERE id = ?',$params);
        }

        private function modifyUser()
        {
            parse_str(file_get_contents("php://input"), $_PUT);

            $user = db::get('SELECT * FROM users WHERE id = ?',[$_PUT['id']]);
            db::insertChangelog('dolgozó módosítva',$user);
            
            $params = [$_PUT['username'],$_PUT['telefonszam'],$_PUT['telephely'],$_PUT['admin'],$_PUT['id']];
            db::execute('UPDATE users SET username = ?, telefonszam = ?, telephely = ?, admin = ? WHERE id = ?',$params);
        }

        private function changeUdvarnev()
        {
            parse_str(file_get_contents("php://input"), $_PUT);
            // changelog
            $udvar = db::get('SELECT * FROM udvar WHERE id = ?',[$_PUT['udvarid']]);
            db::insertChangelog('udvar módosítva',$udvar);

            db::execute('UPDATE udvar SET udvarnev = ? WHERE id = ?',[$_PUT['nev'],$_PUT['udvarid']]);   
        }

        private function modifySzervezet()
        {
            parse_str(file_get_contents("php://input"), $_PUT);
            $params = [$_PUT['nev'],$_PUT['hely'],$_PUT['email'],$_PUT['telefonszam']];
            db::execute('UPDATE menhely SET nev = ?, hely = ?, email = ?, telefonszam = ?',$params);

            // changelog
            db::insertChangelog('szervezet módosítva',[]);
        }

        /* delete */
        private function deleteRequests()
        {
            parse_str(file_get_contents("php://input"), $_DELETE);
            switch($_DELETE['action'])
            {
                case 'delete-worker':
                    $this->deleteWorker();
                    break;
                case 'delete-telephely':
                    $this->checkDogInTelephely();
                    break;
                case 'delete-udvar':
                    $this->deleteUdvar();
                    break;
            }
        }

        private function deleteUdvar()
        {
            parse_str(file_get_contents("php://input"), $_DELETE);

            // changelog
            $udvar = db::get('SELECT * FROM udvar WHERE id = ?',[$_DELETE['udvarid']]);
            db::insertChangelog('udvar törölve',$udvar);

            // bugfix - kennel törlés changelognál a talált udvar null
            startsession();
            $_SESSION['udvar'] = $udvar;
            $this->data['MENTETT UDVAR'] = $_SESSION['udvar'];

            // udvarban lévő kennelek
            $kennelek = db::getall("SELECT * FROM kennel WHERE udvarid = ?",[$_DELETE['udvarid']]);
            if (count($kennelek) > 0)
            {
                foreach($kennelek as $kennel)
                {
                    $this->data['kennelek'][] = $kennel['id'];
                }
            }
            else
            {
                $this->data['kennelek'] = false;
            }

            db::execute('DELETE FROM udvar WHERE id = ?',[$_DELETE['udvarid']]);
        }

        private function checkDogInTelephely()
        {
            parse_str(file_get_contents("php://input"), $_DELETE);
            $telephely = db::get('SELECT * FROM telephely WHERE id = ?',[$_DELETE['id']]);
            $sql = db::getall("SELECT * FROM kutyak WHERE telephely = ?",[$telephely['nev']]);

            if (count($sql) > 0)
            {
                $this->status = false;
                $this->data['msg'] = count($sql)." db kutya van bejegyezve erre a telephelyre!";
                return;
            }

            // changelog
            $telephely = db::get('SELECT * FROM telephely WHERE id = ?',[$_DELETE['id']]);
            db::insertChangelog('telephely törölve',$telephely);

            db::execute('DELETE FROM telephely WHERE id = ?',[$_DELETE['id']]);
        }

        private function deleteWorker()
        {
            parse_str(file_get_contents("php://input"), $_DELETE);

            $user = db::get('SELECT * FROM users WHERE id = ?',[$_DELETE['id']]);
            db::insertChangelog('dolgozó törölve',$user);

            db::execute("DELETE FROM users WHERE id = ?",[$_DELETE['id']]);
        }

        /* post */
        private function postRequests()
        {
            switch(post('action'))
            {
                case 'check-phone':
                    $this->checkphone();
                    break;
                case 'upload-worker':
                    $this->uploadWorker();
                    break;
                case 'check-email':
                    $this->checkemail();
                    break;
                case 'upload-telephely':
                    $this->uploadTelephely();
                    break;
                case 'upload-udvar':
                    $this->uploadUdvar();
                    break;
            }
        }

        private function uploadUdvar()
        {
            $count = db::count("SELECT COUNT(*) FROM udvar WHERE udvarnev LIKE ?",['%Új udvar%']);
            $nevNelkuliUdvarok = $count == 0 ? '' : $count;
            $udvarnev = "Új udvar $nevNelkuliUdvarok";
            
            db::execute('INSERT INTO udvar (telephelyid,udvarnev) VALUES (?,?)',[post('telephelyid'),$udvarnev]);

            // changelog
            $udvar = db::get('SELECT * FROM udvar WHERE id = ?',[db::getlastInsertId()]);
            db::insertChangelog('udvar létrehozva',$udvar);

        }

        private function uploadTelephely()
        {
            $params = [post('nev'),post('cim'),post('email'),post('telefonszam')];
            db::execute('INSERT INTO telephely (nev,hely,email,telefonszam) VALUES (?,?,?,?)',$params);

            // changelog
            $telephely = db::get('SELECT * FROM telephely WHERE id = ?',[db::getlastInsertId()]);
            db::insertChangelog('telephely létrehozva',$telephely);

        }

        private function checkemail()
        {
            if (!filter_var(post('email'),FILTER_VALIDATE_EMAIL))
            {
                $this->status = false;
            }
        }

        private function uploadWorker()
        {
            $sc = password_hash(post('password'),PASSWORD_DEFAULT);
            
            $params = [post('nev'),$sc,post('telefonszam'),post('telephely'),post('admin')];
            db::execute('INSERT INTO users (username,password,telefonszam,telephely,admin) VALUES(?,?,?,?,?)',$params);

            $user = db::get('SELECT * FROM users WHERE id = ?',[db::getlastInsertId()]);
            db::insertChangelog('dolgozó létrehozva',$user);
        }

        private function checkphone()
        {
            // érvényesség
            $onlynum = '/^\d+$/';
            if (!preg_match($onlynum,post('telefonszam')))
            {
                $this->status = false;
                return;
            }

            // foglaltság
            $sql = db::getall("SELECT * FROM users WHERE telefonszam = '$_POST[telefonszam]'");
            if (count($sql) != 0)
            {
                $this->status = false;
                $this->data['msg'] = 'Ez a telefonszám már foglalt!';
                return;
            }
        }


        /* get */
        private function getRequests()
        {
            switch(get('action'))
            {
                case 'get-telephely':
                    $this->gettelephelyek();
                    break;
                case 'get-workers':
                    $this->getWorkers();
                    break;
                case 'get-szervezet-adatok':
                    $this->getSzervezetAdatok();
                    break;
                case 'get-udvarok':
                    $this->getUdvarok();
                    break;
                case 'get-changelog':
                    $this->getChangelog();
                    break;
                case 'get-imageroot':
                    $this->getImageRoot();
                    break;
                case 'get-udvar':
                    $this->getUdvar();
                    break;
                case 'get-kennel':
                    $this->getKennel();
                    break;
                case 'get-telephely-id':
                    $this->getTelephely();
                    break;
                
            }
        }

        private function getTelephely()
        {
            $telephely = db::get('SELECT * FROM telephely WHERE id = ?',[get('telephelyid')]);
            $this->data['telephely'] = $telephely;
        }

        private function getKennel()
        {
            $kennel = db::get('SELECT * FROM kennel WHERE id = ?',[get('kennelid')]);
            $this->data['kennel'] = $kennel;
        }

        private function getUdvar()
        {
            $udvar = db::get('SELECT * FROM udvar WHERE id = ?',[get('udvarid')]);
            $this->data['udvar'] = $udvar;
        }

        private function getImageRoot()
        {
            global $imageRoot_reliative;
            $this->data['root'] = $imageRoot_reliative;
        }

        private function getChangelog()
        {
            if (!isset($_GET['userid']) && !isset($_GET['category']))
            {
                $changelogs = db::getall('SELECT * FROM changelog ORDER BY id DESC');
            }
            else
            {
                $changelogs = db::getall('SELECT * FROM changelog WHERE userid = ? AND category = ?',[get('userid'),get('category')]);
            }

            foreach($changelogs as $cl)
            {
                $user = db::get('SELECT username FROM users WHERE id = ?',[$cl['userid']]);
                $result = ['user' => $user, 'changelog' => $cl];
                $this->data['changelogs'][] = $result;
            }
        }

        private function getUdvarok()
        {
            $udvarok = db::getall('SELECT * FROM udvar');
            $this->data['udvarok'] = $udvarok;
        }

        private function getSzervezetAdatok()
        {
            $result = db::get('SELECT * FROM menhely');
            $this->data['szervezet'] = $result;
        }

        private function getWorkers()
        {
            $columns = "id,username,telefonszam,telephely,admin";
            $result = db::getall("SELECT $columns FROM users ORDER BY username");
            $this->data['workers'] = $result;
        }

        private function gettelephelyek()
        {
            $result = db::getall('SELECT * FROM telephely ORDER BY id DESC');
            $this->data['telephelyek'] = $result;
        }

    }

    new dataapi();
?>