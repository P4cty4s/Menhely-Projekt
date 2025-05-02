<?php

    class db
    {
        private static $pdo = null;
    
        private static function conn()
        {
            global $database, $db_host, $db_user, $db_pass, $db_charset;

            if (self::$pdo === null)
            {
                self::$pdo = new PDO("mysql:host=$db_host;dbname=$database;charset=$db_charset", $db_user, $db_pass, [
                    PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
                    PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC
                ]);
            }
            return self::$pdo;
        }
    
        public static function getall($query, $params = [])
        {
            $stmt = self::conn()->prepare($query);
            $stmt->execute($params);
            return $stmt->fetchAll();
        }
    
        public static function get($query, $params = [])
        {
            $stmt = self::conn()->prepare($query);
            $stmt->execute($params);
            return $stmt->fetch();
        }
    
        public static function execute($query, $params = [])
        {
            $stmt = self::conn()->prepare($query);
            return $stmt->execute($params);
        }
    
        public static function getlastInsertId()
        {
            return self::conn()->lastInsertId();
        }

        public static function count($query, $params = [])
        {
            $stmt = self::conn()->prepare($query);
            $stmt->execute($params);
            return (int)$stmt->fetchColumn();
        }

        public static function getUser(int $userid)
        {
            $user = db::get('SELECT * FROM users WHERE id = ?',[$userid]);
            return $user;
        }

        public static function insertChangelog(string $category, array $data)
        {
            $user = db::get('SELECT * FROM users WHERE id = ?',[$_COOKIE['userid']]);
            $arr = explode(' ',$category);
            $cat = $arr[0];
            $action = $arr[1];

            switch($cat)
            {
                case 'kutya': $msg = $data['nev'].'('.$data['id'].') '.$action.' '.$user['username'].'('.$user['id'].') által'; break;
                case 'udvar': $msg = $data['udvarnev'].'('.$data['id'].') '.$action.' '.$user['username'].'('.$user['id'].') által';break;
                case 'szervezet': $msg = "Szervezet módosítva ".$user['username'].'('.$user['id'].') által'; break;
                case 'telephely': $msg = $data['nev'].'('.$data['id'].') telephely '.$action.' '.$user['username'].'('.$user['id'].') által'; break;
                case 'dolgozó': $msg = $data['username'].'('.$data['id'].') '.$action.' '.$user['username'].'('.$user['id'].') által'; break;

                case 'kennel' :
                    $udvar = $data['udvar'];
                    $kennel = $data['kennel'];

                    $msg = $udvar['udvarnev'].'('.$udvar['id'].')-'.$kennel['kennelszam'].' kennel('.$kennel['id'].') '.$action.' '.$user['username'].'('.$user['id'].') által';
                    break;
            }

            $today = (new DateTime('now'))->format('Y-m-d H:i:s');

            db::execute("INSERT INTO changelog (category,userid,msg,date) VALUES(?,?,?,?)",[$category,$user['id'],$msg,$today]);
        }

    }
    


?>