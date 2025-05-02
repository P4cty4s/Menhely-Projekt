<?php

    require_once "../config.php";
    require_once HTML;

    $html = new html('Új telephely létrehozása');
    $html->setonlylogged(true);
    $html->navigateifnotlogged('login.php');

    // import css

    // import js
    $html->addjs(JS_SITE."ujtelephely.js");

    $html->render();
?>

<div class="container contentMarginTop">
    <form id="ujtelephely">
        <h3 class="form-title">Új telephely létrehozása</h3>
        <input id="nev" type="text" data-label="Név" req>
        <input id="telefonszam" type="text" data-label="Telefonszám" req>
        <input id="email" type="email" data-label="Email" req>
        <input id="cim" type="text" data-label="Cím" req>
        <button trigger id="uploadBtn">Feltöltés</button>
    </form>
</div>

<?php $html->close(); ?>