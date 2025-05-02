<?php

    require_once "../config.php";
    require_once HTML;

    $html = new html('Szervezeti beállítások');
    $html->setonlylogged(true);
    $html->navigateifnotlogged('login.php');

    // import js
    $html->addjs(JS_SITE."beallitasok.js");

    // import css

    $html->render();
?>

    <div class="container contentMarginTop">

        <form id="szervezet">
            <h3 class="form-title">Szervezet adatai</h3>
            <input id="nev" type="text" data-label="Szervezet neve" req>
            <input id="hely" type="text" data-label="Szervezet címe" req>
            <input id="email" type="email" data-label="Szervezet Email címe" req>
            <input id="telefonszam" type="text" data-label="Szervezet telefonszáma" req>
            <button trigger id="szervezet-modify">Módosítás</button>
        </form>

    </div>


<?php $html->close(); ?>