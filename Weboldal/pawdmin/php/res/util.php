<?php

    // data functions

        // start
        function startsession() { session_status() == PHP_SESSION_NONE && session_start(); }

        //get data
        function get(string $key) { return $_GET[$key]; }
        function post(string $key) { return $_POST[$key]; }
        function session(string $key) { return $_SESSION[$key]; }
        function cookie(string $key) { return $_COOKIE[$key]; }

        // create data
        function setsession(string $key, $value) { $_SESSION[$key] = $value; }
        function makecookie(string $key, $value, int $time) { setcookie($key,$value,time()+$time,"/"); }

    // date

        function getToday() { return (new DateTime('today'))->format('Y-m-d'); }
?>