<?php
    require_once "../config.php";
    require HTML;

    $html = new html('Bejelentkezés');

    // import css
    $html->addcss(CSS_SITE."login.css");

    // import js
    $html->addjs(JS_SITE."login.js");

    $html->render();

?>

    <form id="login">
        <h3 class="form-title">Bejelentkezés</h3>
        <input id="username" type="text" data-label="Felhasználónév" req>
        <input id="password" type="password" data-label="Jelszó" req>
        <button id="loginBtn" class="b-light-brown" trigger>Bejelentkezés</button>
    </form>

<?php $html->close(); ?>