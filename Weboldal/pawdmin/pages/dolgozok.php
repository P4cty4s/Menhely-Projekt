<?php

    require_once "../config.php";
    require_once HTML;

    $html = new html('Dolgozók');
    $html->setonlylogged(true);
    $html->navigateifnotlogged('login.php');

    // import js
    $html->addjs(JS_SITE."dolgozok.js");

    // import css


    $html->render();
?>

<div class="container">
    <a href="ujdolgozo.php"><h5>Új dolgozó hozzáadása</h5></a>
    <table id="dolgozok" class="c-table"></table>
</div>

<?php $html->close(); ?>