<?php

    require_once "../config.php";
    require_once HTML;

    $html = new html('Dolgozó módosítása');
    $html->setonlylogged(true);
    $html->navigateifnotlogged('login.php');

    // import js
    $html->addjs(JS_SITE.'dolgozo-modositas.js');

    // import css

    $html->render();
?>

<div class="container">
    <form id="modify-dolgozo">
        <h3 class="form-title">Dolgozó módosítása</h3>
        <input id="username" type="text" data-label="Név" req>
        <input id="telefonszam" type="text" data-label="Telefonszám" req>
        <select id="telephely" data-label="Telephely"></select>
        <select id="admin" data-label="Jogosultság">
            <option value="0">Dolgozó</option>
            <option value="1">Admin</option>
        </select>
        <button id="uploadBtn" trigger>Feltöltés</button>
    </form>
</div>

<?php $html->close(); ?>