<?php

    require_once "../config.php";
    require_once HTML;

    $html = new html('Dolgozó hozzáadása');
    $html->setonlylogged(true);
    $html->navigateifnotlogged('login.php');

    // import js
    $html->addjs(JS_SITE."ujdolgozo.js");

    // import css


    $html->render();
?>

<div class="container contentMarginTop">
    <form id="ujdolgozo">
        <h3 class="form-title">Dolgozó hozzáadása</h3>
        <input id="nev" type="text" data-label="Név" req>
        <input id="telefonszam" type="text" data-label="Telefonszám" req>
        <input id="password" type="password" data-label="Jelszó" req>
        <input id="password2" type="password" data-label="Jelszó újra" req>
        <select id="telephely" data-label="Telephely"></select>
        <select id="admin" data-label="Jogosultság">
            <option value="0">Dolgozó</option>
            <option value="1">Admin</option>
        </select>
        <button id="uploadBtn" trigger>Feltöltés</button>
    </form>
</div>

<?php $html->close(); ?>