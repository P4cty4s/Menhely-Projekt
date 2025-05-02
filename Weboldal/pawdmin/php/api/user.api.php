<?php
    require_once "../../config.php";
    require_once PHP_UTIL;
    require_once BASEAPI;
    require_once DATABASE;

    class userapi extends baseapi
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
                case 'GET':  $this->getRequests(); break;
                case 'POST': $this->postRequests(); break;
            }
        }

        /* get */
        private function getRequests()
        {
            switch(get('action'))
            {
                case 'get-user':
                    $this->getUser();
                    break;
            }
        }

        private function getUser()
        {   
            $user = db::get('SELECT username,telefonszam,telephely,admin FROM users WHERE id = ?',[get('id')]);
            $this->data['user'] = $user;
        }

        /* post */
        private function postRequests()
        {
            switch(post('action'))
            {
                case 'check-login':
                    $this->checklogin();
                    break;
            }
        }

        private function checklogin()
        {
            $user = db::get("SELECT * FROM users WHERE username = ?",[post('username')]);
            if ($user == false ) { $this->status = false; return; }
            if (!password_verify(post('password'),$user['password'])) { $this->status = false; return; }
            
            makecookie('userid',$user['id'],60*60);
        }

    }

    new userapi;
?>