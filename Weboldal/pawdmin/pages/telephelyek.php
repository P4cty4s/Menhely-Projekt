<?php

    require_once "../config.php";
    require_once HTML;

    $html = new html('Telephelyek');
    $html->setonlylogged(true);
    $html->navigateifnotlogged('login.php');

    // import js
    $html->addjs(JS_SITE."telephelyek.js");

    // import css

    $html->render();
?>

    <div class="container contentMarginTop">
        <a href="ujtelephely.php">Telephely módosítása</a>
        <table id="telephelyek" class="c-table"></table>
    </div>

<?php $html->close(); ?>