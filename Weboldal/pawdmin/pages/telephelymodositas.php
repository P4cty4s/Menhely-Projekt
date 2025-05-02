<?php

    require_once "../config.php";
    require HTML;

    $html = new html('Telephely módosítása');

    // site
    $html->setonlylogged(true);
    $html->navigateifnotlogged('login.php');

    // import js
    $html->addjs(JS_SITE."telephelymodositas.js");

    // import css

    $html->render();
?>

<div class="container contentMarginTop">
    <form id="telephely">
        <h3 class="form-title">Új telephely létrehozása</h3>
        <input id="nev" type="text" data-label="Név" req>
        <input id="telefonszam" type="text" data-label="Telefonszám" req>
        <input id="email" type="email" data-label="Email" req>
        <input id="hely" type="text" data-label="Cím" req>
        <button trigger id="uploadBtn">Feltöltés</button>
    </form>
</div>

<?php $html->close(); ?>