<?php
    require_once "../../config.php";
    require_once PHP_UTIL;

    class baseapi
    {
        protected bool $status;
        protected array $data;
        protected array $response;

        public function __construct()
        {
            $this->status = true;
            $this->data = [];
            $this->response = [];
        }

        protected function sendresponse()
        {
            header('Content-Type: application/json');
            $this->response =
            [
                'status' => $this->status,
                'data' => $this->data
            ];
            echo json_encode($this->response);
        }

        protected function getUserid() { return cookie('userid'); }
        protected function getToday() { return (new DateTime('today'))->format('Y-m-d'); }
    }   

?>