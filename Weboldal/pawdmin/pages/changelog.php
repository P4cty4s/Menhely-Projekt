<?php

    require_once "../config.php";
    require_once HTML;

    $html = new html('Előzmények');

    // import js
    $html->addjs(JS_SITE."changelog.js");
    $html->addjs(JS_ELEMENT."sidebar.js");

    // import css
    $html->addcss(CSS_ELEMENT."sidebar.css");
    $html->addcss(CSS_SITE."kutyatabla.css");

    $html->render();
?>

<div class="contentMarginTop mobile-buttons"><button sidebar>Szűrők</button></div>

<div class="sidebar start-align">

        <h2 class="sidebar-title">Előzmények szűrése</h2>

        <div class="sidebar-box">
            <h5 class="">Dolgozó</h5>
            <select id="userid" selector></select>
        </div>

        <div class="sidebar-box">
            <h5>Kategória</h5>
            <select id="category" selector>
               
                <option value="kutya létrehozva">kutya létrehozva</option>
                <option value="kutya módosítva">kutya módosítva</option>
                <option value="kutya törölve">kutya törölve</option>

                <option value="kennel létrehozva">kennel létrehozva</option>
                <option value="kennel módosítva">kennel módosítva</option>
                <option value="kennel törölve">kennel törölve</option>

                <option value="udvar létrehozva">udvar létrehozva</option>
                <option value="udvar módosítva">udvar módosítva</option>
                <option value="udvar törölve">udvar törölve</option>

                <option value="telephely létrehozva">telephely létrehozva</option>
                <option value="telephely módosítva">telephely módosítva</option>
                <option value="telephely törölve">telephely törölve</option>

                <option value="dolgozó létrehozva">dolgozó létrehozva</option>
                <option value="dolgozó módosítva">dolgozó módosítva</option>
                <option value="dolgozó törölve">dolgozó törölve</option>

            </select>
        </div>

</div>

<table class="c-table start-align" id="changelog"></table>

<?php $html->close(); ?>