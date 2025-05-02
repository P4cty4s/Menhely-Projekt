<?php
    require_once "../../config.php";
    require_once PHP_UTIL;
    require_once BASEAPI;
    require_once DATABASE;
    require_once CHANGELOG;

    class kutyakapi extends baseapi
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
                case 'GET' : $this->getRequests(); break;
                case 'POST' : $this->postRequests(); break;
                case 'PUT' : $this->putRequests();  break;
                case 'DELETE' : $this->deleteRequests(); break;
            }

        }

        /* delete */
        private function deleteRequests()
        {
            parse_str(file_get_contents("php://input"),$_DELETE);
            switch($_DELETE['action'])
            {
                case 'delete-img':
                    $this->deleteImg();
                    break;
                case 'delete-kennel':
                    $this->deleteKennel();
                    break;
                case 'delete-dog':
                    $this->deleteDog();
            }
        }

        private function deleteDog()
        {
            parse_str(file_get_contents("php://input"),$_DELETE);
            $kutya = db::get('SELECT * FROM kutyak WHERE id = ?',[$_DELETE['kutyaid']]);
            db::insertChangelog('kutya törölve',$kutya);

            db::execute('UPDATE kutyak SET visible = ? WHERE id = ?',[0,$_DELETE['kutyaid']]);
        }

        private function deleteKennel()
        {
            startsession();
            parse_str(file_get_contents("php://input"),$_DELETE);
            $kennelek = $_DELETE['kennelek'];

            foreach($kennelek as $kennelid)
            {   
                // kutyák kennel-idjának resetelése
                $kennel = db::get('SELECT * FROM kennel WHERE id = ?',[$kennelid]);
                $kutyak = $this->getKutyakInKennel($kennel['kutyak']);
                if ($kutyak[0] != false)
                {
                    foreach($kutyak as $kutya)
                    {
                        db::execute('UPDATE kutyak SET kennel = 0 WHERE id = ?',[$kutya['id']]);
                    }
                }
                
                // kennel törlése - ch
                $kennel = db::get('SELECT * FROM kennel WHERE id = ?',[$kennelid]);
                $udvarSQL = db::get('SELECT * FROM udvar WHERE id = ?',[$kennel['udvarid']]);
                $udvar = $udvarSQL == false ? session('udvar') : $udvarSQL;
                db::insertChangelog('kennel törölve',['kennel' => $kennel, 'udvar' => $udvar]);

                // kennel törlése
                db::execute('DELETE FROM kennel WHERE id = ?',[$kennelid]);
                $this->reNumberKennel($kennelid);

            }

        }

        private function deleteImg()
        {
            parse_str(file_get_contents("php://input"),$_DELETE);
            global $imageRoot_absolute;

            // kép törlése mappából
            $img = db::get('SELECT * FROM kutyakep WHERE id = ?',[$_DELETE['id']]);
            if (file_exists($imageRoot_absolute.$img['nev']))
            {
                unlink($imageRoot_absolute.$img['nev']);
            }

            // kép törlése adatbázisból
            db::execute('DELETE FROM kutyakep WHERE id = ?',[$_DELETE['id']]);

        }

        /* put */
        private function putRequests()
        {
            parse_str(file_get_contents("php://input"), $_PUT);
            switch($_PUT['action'])
            {
                case 'switch-index':
                    $this->switchIndexImg();
                    break;
                case 'change-kennel':
                    $this->changeKennel();
                    break;
            }
        }

        private function changeKennel()
        {
            parse_str(file_get_contents("php://input"), $_PUT);
            
            db::execute("UPDATE kutyak SET kennel = ? WHERE id = ?",[$_PUT['kennel'],$_PUT['kutyaid']]);
            $_PUT['kennel'] != 0 && $this->generateKennelKutyakString(true,$_PUT['kennel'],$_PUT['kutyaid']);
            if (isset($_PUT['regiKennelid']) && $_PUT['regiKennelid'] != 'false')
            {
                $this->generateKennelKutyakString(false,$_PUT['regiKennelid'],$_PUT['kutyaid']);
            }

            // changelog
            if ($_PUT['kennel'] != $_PUT['regiKennelid'])
            {
                $kutya = db::get('SELECT * FROM kutyak WHERE id = ?',[$_PUT['kutyaid']]);
                db::insertChangelog('kutya módosítva',$kutya);
            }

        }

        private function generateKennelKutyakString(bool $add, $kennelid,int $kutyaid)
        {
            $kennel = db::get('SELECT * FROM kennel WHERE id = ?',[$kennelid]);
            $kennelkutyak = array_filter(explode(";", $kennel['kutyak']), function($elem)
            {
                return $elem !== '';
            });

            if ($add)
            {
                $kennelkutyak[] = $kutyaid;
            }
            else
            {
                foreach ($kennelkutyak as $key => $elem)
                {
                    if ($elem == $kutyaid)
                    {
                        unset($kennelkutyak[$key]);
                        $this->data['remove'][] = "elvéve $key elem";
                        break;
                    }
                }
            }
            $this->data['result'] = $kennelkutyak;
            $result = implode(';',$kennelkutyak);
            db::execute('UPDATE kennel SET kutyak = ? WHERE id = ?',[$result,$kennelid]);
        }

        private function switchIndexImg()
        {
            parse_str(file_get_contents("php://input"), $_PUT);

            // changelog
            $kutya = db::get('SELECT * FROM kutyak WHERE id = ?',[$_PUT['kutyaid']]);
            db::insertChangelog('kutya módosítva',$kutya);

            db::execute('UPDATE kutyak SET indexkepid = ? WHERE id = ?',[$_PUT['kepid'],$_PUT['kutyaid']]);
        }

        /* post */
        private function postRequests()
        {
            switch(post('action'))
            {
                case 'check-chipszam':
                    $this->checkchipszam();
                    break;
                case 'upload-dog':
                    $this->uploadDog();
                    break;
                case 'modify-dog':
                    $this->modifyDog();
                    break;
                case 'new-kennel':
                    $this->uploadKennel();
                    break;    
            }
        }

        private function uploadKennel()
        {
            
            $udvarid = post('udvarid');
            $kennelszam = $this->getKennelSzam($udvarid);
            db::execute('INSERT INTO kennel (udvarid,kennelszam,kutyak) VALUES(?,?,?)',[$udvarid,$kennelszam,'']);
            
            // changelog
            $kennel = db::get('SELECT * FROM kennel WHERE id = ?',[db::getlastInsertId()]);
            $udvar = db::get('SELECT * FROM udvar WHERE id = ?',[$kennel['udvarid']]);
            $data = ['kennel' => $kennel, 'udvar' => $udvar];
            db::insertChangelog('kennel létrehozva',$data);

            $this->reNumberKennel($udvarid);
        }

        private function reNumberKennel(int $udvarid)
        {
            $count = 1;
            $kennelek = db::getall('SELECT * FROM kennel WHERE udvarid = ? ORDER BY kennelszam',[$udvarid]);
            foreach($kennelek as $kennel)
            {
                db::execute('UPDATE kennel SET kennelszam = ? WHERE id = ?',[$count,$kennel['id']]);
                $count++;
            }
        }

        private function getKennelSzam(int $udvarid)
        {
            $kennel = db::get('SELECT * FROM kennel WHERE udvarid = ? ORDER BY kennelszam DESC LIMIT 1',[$udvarid]);
            if (!$kennel) return 1;
            
            return $kennel['kennelszam'] + 1;
        }

        private function modifyDog()
        {
            $d = json_decode($_POST['kutya-adat'],true);

            // kutya módosítás - ch
            $modify = db::get('SELECT * FROM kutyak WHERE id = ?',[$d['id']]);
            db::insertChangelog('kutya módosítva',$modify);

            // kutya módosítás
            db::execute('UPDATE kutyak SET visible=?, status=?, regszam=?,nev=?,chipszam=?,ivar=?,meret=?,szuletes=?,bekerules=?,ivaros=?,telephely=?,foglalt=? WHERE id=?',
            [$d['visible'],$d['status'],$d['regszam'], $d['nev'], $d['chipszam'], $d['ivar'], $d['meret'], $d['szuletes'], $d['bekerules'], $d['ivaros'], $d['telephely'],
            $d['foglalt'],$d['id']]); 


            // képek
            if (isset($_FILES['images']))
            {
                global $imageRoot_absolute;
                $kutyaid = $d['id'];            

                // Végigmegyünk a feltöltött fájlokon
                foreach ($_FILES['images']['tmp_name'] as $index => $tmpName)
                {
                    $filename = $kutyaid."-".$_FILES['images']['name'][$index]; // Fájl neve
                    $targetPath = $imageRoot_absolute.$filename; // Célútvonal
                    
                    // Fájl áthelyezése a célmappába
                    if (move_uploaded_file($tmpName, $targetPath))
                    {
                        // Adatbázisba mentés
                        $columns = "(kutyaid, nev)";
                        $params = [$kutyaid, $filename];
                        db::execute("INSERT INTO kutyakep $columns VALUES (?, ?)", $params);
                    }
                }

                $this->data['img'] = 'Képek feltöltve';
            }

        }

        private function uploadDog()
        {
            $d = json_decode(post('kutya-adat'),true);
            
            // kutya felöltés
            $columns = "(regszam,nev,chipszam,ivar,meret,szuletes,bekerules,ivaros,telephely,foglalt,kennel,indexkepid,status,visible)";
            $params = [
                $d['regszam'],
                $d['nev'],
                $d['chipszam'],
                $d['ivar'],
                $d['meret'],
                $d['szuletes'],
                $d['bekerules'],
                $d['ivaros'],
                $d['telephely'],
                0,
                0,
                0,
                $d['status'],
                $d['visible']
            ];
            $values = "(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,?)";
            $query = "INSERT INTO kutyak $columns VALUES $values";
            db::execute($query, $params);
            $this->data['kutyaid'] = db::getlastInsertId();
            $this->status = true;

            // kutya feltöltése - ch
            $uploadedDog = db::get('SELECT * FROM kutyak WHERE id = ?',[db::getlastInsertId()]);
            db::insertChangelog('kutya létrehozva',$uploadedDog);

            // kép feltöltése
            if (isset($_FILES['images']))
            {
                global $imageRoot_absolute;
                $kutyaid = db::getlastInsertId();            

                // Végigmegyünk a feltöltött fájlokon
                foreach ($_FILES['images']['tmp_name'] as $index => $tmpName)
                {
                    $filename = $kutyaid."-".$_FILES['images']['name'][$index]; // Fájl neve
                    $targetPath = $imageRoot_absolute.$filename; // Célútvonal
                    // Fájl áthelyezése a célmappába
                    if (move_uploaded_file($tmpName, $targetPath))
                    {
                        // Adatbázisba mentés
                        $this->data['path'] = $targetPath;
                        $columns = "(kutyaid, nev)";
                        $params = [$kutyaid, $filename];
                        db::execute("INSERT INTO kutyakep (kutyaid, nev) VALUES (?, ?)", $params);
                    }
                }

                $this->data['img'] = 'Képek feltöltve';
            }
        }

        private function checkchipszam()
        {
            $chipszamRegex = '/^\d{15}$/';
            $this->status = preg_match($chipszamRegex,post('chipszam'));
        }

        /* get */
        private function getRequests()
        {
            switch(get('action'))
            {
                case 'get-dogs':
                    $this->getdogs('array',$_GET['hasoffset'],$_GET['needimg']);
                    break;
                case 'get-korrange':
                    $this->getkorrange();
                    break;
                case 'get-dogs-count':
                    $this->getdogs('count');
                    break;
                case 'get-kennelek':
                    $this->getKennelek();
                    break;
                case 'get-images':
                    $this->getImages();
                    break;
            }
        }

        private function getImages()
        {
            $result = [];

            $kutyak = db::getall('SELECT * FROM kutyak ORDER BY id DESC');
            foreach($kutyak as $kutya)
            {
                $kepek = db::getall('SELECT * FROM kutyakep WHERE kutyaid = ?',[$kutya['id']]);
                count($kepek) > 0 && $result[] = ['kutya' => $kutya['nev'], 'kepek' => $kepek];
            }
            $this->data['images'] = $result;
        }

        private function getKennelek()
        {

            if (get('telephelyid') == 'false')
            {
                $telephely = db::get('SELECT * FROM telephely ORDER BY id DESC LIMIT 1');
                $telephelyid = $telephely['id'];
                $this->data['telephely'] = 'false';
            }
            else
            {
                $telephely = db::get('SELECT * FROM telephely WHERE id = ?',[get('telephelyid')]);
                $telephelyid = get('telephelyid');
            }

            $udvarok = db::getall('SELECT * FROM udvar WHERE telephelyid = ? ORDER BY id DESC',[$telephelyid]);
            foreach($udvarok as $i => $udvar)
            {
                $kennelek = db::getall('SELECT * FROM kennel WHERE udvarid = ?',[$udvar['id']]);
                $udvarok[$i]['kennelek'] = $kennelek;
                foreach($kennelek as $j => $kennel)
                {
                    $kutyak = $this->getKutyakInKennel($kennel['kutyak']);
                    $udvarok[$i]['kennelek'][$j]['kutyak'] = $kutyak;
                }
            }
            $this->data['udvarok'] = $udvarok;
            $this->data['telephely'] = $telephely;
        }

        private function getKutyakInKennel(string $kutyak)
        {
            $result = [];
            $ids = explode(';',$kutyak);
            foreach($ids as $id)
            {
                $kutya = db::get('SELECT * FROM kutyak WHERE id = ?',[$id]);
                $result[] = $kutya;
            }
            return $result;
        }

        private function getkorrange()
        {
            $fiatal = db::get('SELECT * FROM kutyak ORDER BY szuletes DESC LIMIT 1');
            $idos = db::get('SELECT * FROM kutyak ORDER BY szuletes LIMIT 1');

            $ma = new DateTime('today');
            $fiatal_szuletes = new DateTime($fiatal['szuletes']);
            $idos_szuletes = new DateTime($idos['szuletes']);

            $legfiatalabb = ceil(($ma->diff($fiatal_szuletes)->y*12+$ma->diff($fiatal_szuletes)->m)/12);
            $legidosebb = ceil(($ma->diff($idos_szuletes)->y*12+$ma->diff($idos_szuletes)->m)/12);

            $this->data['korrange'] = ['fiatal' => $legfiatalabb, 'idos' => $legidosebb];
        }

        private function getKennelnev(int $kennelid)
        {
            if ($kennelid == 0) return 'nincs kennelje';

            $kennel = db::get('SELECT * FROM kennel WHERE id = ?',[$kennelid]);
            $udvar = db::get('SELECT * FROM udvar WHERE id = ?',[$kennel['udvarid']]);
            return $udvar['udvarnev']." - ".$kennel['kennelszam'];
        }

        private function getdogs(string $return, bool $hasoffset=false, bool $needimg=false)
        {
            if($return == 'array')
            {
                $query = $this->generateQuery($hasoffset);
                $kutyak = db::getall($query);
                foreach($kutyak as $kutya)
                {
                    $kor = $this->getkor($kutya['szuletes']);
                    $kutya['kor'] = $kor;

                    if ($needimg)
                    {
                        $images = db::getall('SELECT * FROM kutyakep WHERE kutyaid = ? AND id != ?',[$kutya['id'],$kutya['indexkepid']]);
                        $indexkep = db::get('SELECT * FROM kutyakep WHERE id = ?',[$kutya['indexkepid']]);
                        $kutya['kepek'] = $images;
                        $kutya['indexkep'] = $indexkep;
                    }

                    isset($_GET['kennel']) && $kutya['kennelnev'] = $this->getKennelnev($kutya['kennel']);
                    isset($_GET['needKennel']) && $kutya['kennelnev'] = $this->getKennelnev($kutya['kennel']);

                    $this->data['kutyak'][] = $kutya;
                }
                $this->data['query'] = $query;
            }
            else
            {
                $query = $this->generateQuery(false);
                $kutyak = db::getall($query);
                $count = count($kutyak);
                $this->data['count'] = $count;
            }
        }

        private function getkor($szuletesstr)
        {
            $ma = new DateTime();
            $szuletes = new DateTime($szuletesstr);
            $result = ceil(($ma->diff($szuletes)->y*12+$ma->diff($szuletes)->m)/12);

            return $result;
        }

        private function generateQuery(bool $hasoffset=true)
        {
            $query = "SELECT * FROM kutyak"; 
            $conditions = [];

            // id
            if (isset($_GET['id']))
            {
                $id = $_GET['id'];
                $conditions[] = "id = $id";
            }

            // kennel
            if (isset($_GET['kennel']))
            {
                $kennelid = $_GET['kennel'];
                $conditions[] = "kennel = $kennelid";
            }

            // visible
            if (isset($_GET['visible']) && $_GET['visible'][0] != 'false')
            {
                $visible = $_GET['visible'][0];
                $conditions[] = "visible = $visible";
            }

            // foglalt
            if (isset($_GET['foglalt']) && $_GET['foglalt'][0] != 'false')
            {
                $foglalt = $_GET['foglalt'][0];
                $conditions[] = "foglalt = $foglalt";
            }

            //nev
            if (isset($_GET['nev']))
            {
                $nev = $_GET['nev'][0];

                $conditions[] = "nev LIKE '%$nev%'";
            }

            //chipszam
            if (isset($_GET['chipszam']))
            {
                $chipszam = $_GET['chipszam'][0];
                $conditions[] = "chipszam LIKE '$chipszam%'";
            }

            //regszam
            if (isset($_GET['regszam']))
            {
                $regszam = $_GET['regszam'][0];
                $conditions[] = "regszam LIKE '$regszam%'";
            }

            // telephely
            if (isset($_GET['telephely']))
            {
                $telephelyValues = array_map(fn($m) => "'" . addslashes($m) . "'", $_GET['telephely']);
                $conditions[] = "telephely IN (" . implode(", ", $telephelyValues) . ")";
            }

            // születés
            if (isset($_GET['kor']))
            {
                $ma = new DateTime(); 
            
                $minkor = (int)$_GET['kor'][0];
                $maxkor = (int)$_GET['kor'][1];
            
                $minimum_szuletes = clone $ma;
                $maximum_szuletes = clone $ma;
            
                $minimum_szuletes->modify("-{$minkor} years");
                $maximum_szuletes->modify("-{$maxkor} years");
            
                $min_date = $minimum_szuletes->format('Y-m-d');
                $max_date = $maximum_szuletes->format('Y-m-d');
                
                $conditions[] = "szuletes BETWEEN '$max_date' AND '$min_date'";
            }
            

            // Méret 
            if (isset($_GET['meret']))
            {
                $meretValues = array_map(fn($m) => "'" . addslashes($m) . "'", $_GET['meret']);
                $conditions[] = "meret IN (" . implode(", ", $meretValues) . ")";
            }

            // Ivar
            if (isset($_GET['ivar']))
            {
                $ivarValues = array_map(fn($i) => "'" . addslashes($i) . "'", $_GET['ivar']);
                $conditions[] = "ivar IN (" . implode(", ", $ivarValues) . ")";
            }
            
            // ivarosság
            if (isset($_GET['ivaros']))
            {
                $ivarValues = array_map(fn($i) => "'" . addslashes($i) . "'", $_GET['ivaros']);
                $conditions[] = "ivaros IN (" . implode(", ", $ivarValues) . ")";
            }

            // status
            if (isset($_GET['status']) && $_GET['status'][0] != 'false')
            {
                $status = $_GET['status'][0];
                $conditions[] = "status = '$status'";
            }

            // Feltételek
            if (!empty($conditions))
            {
                
                $query .= " WHERE " . implode(" AND ", $conditions);
            }
            
            //order
            if (isset($_GET['order']))
            {
                $order = $_GET['order'][0];
            }
            else
            {
                $order = " ORDER BY id DESC";    
            }
            $query .= $order;

            // offset
            if ($hasoffset && isset($_GET['offset']))
            {
                $offset = (int)$_GET['offset'];
                $query .= " LIMIT 20 OFFSET $offset";
            }

            return $query;
        }

    }

    new kutyakapi();

?>