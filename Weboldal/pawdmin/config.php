<?php

    // error reporting
    error_reporting(E_ALL); 
    ini_set('display_errors', 1); 

    // navigation
    $navigation =
    [
        /*[ 'title' => "Kezdőlap", 'href' => "index.php" ],*/
        [ 'title' => "Gazdára váró kutyák", 'href' => "kutyak.php", 'icon' => '<i class="fa-solid fa-paw"></i>'],
        [ 'title' => "Galéria", 'href' => "galeria.php", 'icon' => '<i class="fa-solid fa-images"></i>' ]
    ];

    if (!isset($_COOKIE['userid']))
    {
        $navs =
        [
            [ 'title' => "Bejelentkezés", 'href' => "login.php", 'icon' => '<i class="fa-solid fa-right-to-bracket"></i>' ]
        ];

        $navigation = array_merge($navigation,$navs);

    }
    else
    {
        $navs =
        [
            [ 'title' => "Dolgozói felület", 'href' => "admin.php", 'icon' => '<i class="fa-solid fa-person-digging"></i>' ],
            [ 'title' => "Kijelentkezés", 'href' => "../php/res/logout.php", 'icon' => '<i class="fa-solid fa-right-from-bracket"></i>' ]
        ];

        $navigation = array_merge($navigation,$navs);
    }
    
    // imports

        // a projekt mappa neve
        $projectRoot = "pawdmin";

        // root
        define('ROOT',$_SERVER['DOCUMENT_ROOT']."/".$projectRoot."/");
        define('RELATIVE_ROOT',"../");

        define('PHP',ROOT."php/");
        define('JS',RELATIVE_ROOT."js/");
        define('CSS',RELATIVE_ROOT."css/");
        define('SRC',RELATIVE_ROOT."src/");

        // php
        define('API',PHP."api/");
        define('PHP_RES',PHP."res/");

        // js
        define('JS_RES',JS."res/");
        define('JS_SITE',JS."site/");
        define('JS_ELEMENT',JS."element/");

        // css
        define('CSS_MAIN',CSS."main/");
        define('CSS_ELEMENT',CSS."element/");
        define('CSS_SITE',CSS."site/");

        // tools
        define('HTML',PHP_RES."html.php");
        define('PHP_UTIL',PHP_RES."util.php");
        define('BASEAPI',PHP_RES."base.api.php");
        define('DATABASE',PHP_RES."database.php");
        define('PHP_UTILCLASS',PHP_RES."utilclass.php");
        define('CHANGELOG',PHP_RES."changelog.php");

    // configuration

        // project
        $siteName = "Pawdmin";
        $lang = "hu";
        $imageRoot_reliative = "../../Kutya_kepek/uploads/";
        $imageRoot_absolute = ROOT."../Kutya_kepek/uploads/";
        $imageRoot_project = "../src/images/project/";

        // database

            // local
            $database = "pawdmin";
            $db_host = "localhost";
            $db_user = "root";
            $db_pass = "";
            $db_charset = "utf8mb4";

        // html imports
        $import_css =
        [
            "../src/icons/css/all.min.css",
            CSS_MAIN."jquery-ui.min.css",
            CSS_MAIN."bootstrap.min.css",
            CSS_MAIN."setting.css",
            CSS_MAIN."main.css",
            CSS_MAIN."animation.css",

            CSS_ELEMENT."header.css",
            CSS_ELEMENT."box.css",
            CSS_ELEMENT."form.css",
            CSS_ELEMENT."button.css",
            CSS_ELEMENT."popup.css",
            CSS_ELEMENT."table.css",
            CSS_ELEMENT."tag.css",
            CSS_ELEMENT."hero.css",
            CSS_ELEMENT."footer.css"
        ];
        $import_js =
        [
            JS_RES."jquery.js",
            JS_RES."jquery-ui.min.js",
            JS_RES."functions.js",
            JS_RES."api.js",

            JS_ELEMENT."header.js",
            JS_ELEMENT."form.js" ,
            JS_ELEMENT."popup.js",
            JS_ELEMENT."buttons.js"

        ];
?>