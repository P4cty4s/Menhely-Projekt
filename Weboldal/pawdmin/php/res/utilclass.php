<?php

    require_once "../php/res/database.php";

    class util
    {
        public static function getTelephelyek()
        {
            $telephelyek = db::getall('SELECT * FROM telephely ORDER BY nev');
            return $telephelyek;
        }
    }




?>