<?php

    require_once "../config.php";
    require_once HTML;

    $html = new html('Udvarok rendje');
    $html->setonlylogged(true);
    $html->navigateifnotlogged('login.php');

    // import js
    $html->addjs(JS_ELEMENT."sidebar.js");
    $html->addjs(JS_SITE."udvarok.js");

    // import css
    $html->addcss(CSS_SITE."udvarok.css");
    $html->addcss(CSS_ELEMENT."sidebar.css");

    $html->render();
?>

    <div class="trash drop kennel" data-id="0"><i class="fa-solid fa-trash"></i>Törlés a kennelekből</div>

    <div class="sidebar">
        <h2 class="sidebar-title">Beállítások</h2>
        <div class="sidebar-box" data-type="udvar">
            <h5>Telephelyek</h5>
            <select id="telephely"></select>
        </div>
        <div class="sidebar-box" data-type="kutyak">
            <h5>Kennel nélküli kutyák</h5>
            <div class="sidebar-box" data-type="nev"><input type="text" id="nev" placeholder="keresés névre" selector></div>
            <div class="sidebar-box drop row kennel-nelkuli-kutyak" data-id="0"></div>
        </div>
    </div>

    <div>
        <div class="container">
            <div class="padding">
                <button sidebar>Beállítások</button>
            </div>
            <div id="kennelek" class="row"></div>
        </div>
    </div>

<?php $html->close(); ?>