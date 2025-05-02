<?php

    require_once "../config.php";
    require HTML;

    $html = new html('Kutya táblázat');
    $html->setonlylogged(true);
    $html->navigateifnotlogged('login.php');

    // import js 
    $html->addjs(JS_ELEMENT."sidebar.js");
    $html->addjs(JS_SITE."kutyatabla.js");

    // import css
    $html->addcss(CSS_ELEMENT."sidebar.css");
    $html->addcss(CSS_SITE."kutyatabla.css");

    $html->render();
?>

    <div class="sidebar start-align">
        <h2 class="sidebar-title">Szűrési feltételek</h2>
        <div class="sidebar-box" data-type="order">
            <h5>Rendezés</h5>
            <select id="order" selector>
                <option value="ORDER BY id DESC">Legújabb lakók elől</option>
                <option value="ORDER BY id">Legrégebbi lakók elől</option>
                <option value="ORDER BY szuletes DESC">Legfiatalabb lakók elől</option>
                <option value="ORDER BY szuletes">Legöregebb lakók elől</option>
                <option value="ORDER BY FIELD(ivar,'szuka')">Kanok elől</option>
                <option value="ORDER BY FIELD(ivar,'kan')">Szukák elől</option>
            </select selector>
        </div>

        <div class="sidebar-box" data-type="nev">
            <h5>Szűrés</h5>
            <input type="text" placeholder="kereés névre" id="selector-nev" selector>
        </div>

        <div class="sidebar-box" data-type="chipszam"><input type="text" placeholder="keresés chipszámra" id="selector-chipszam" selector></div>

        <div class="sidebar-box" data-type="regszam"><input type="text" placeholder="keresés regisztrációs számra" id="selector-regszam" selector></div>

        <div class="sidebar-box" data-type="kor"></div>
        <div class="sidebar-box" data-type="meret">
            <h5>Méret</h5>
            <input type="checkbox" data-label="kölyök" data-value="kölyök" selector>
            <input type="checkbox" data-label="kistestű" data-value="kistestű" selector>
            <input type="checkbox" data-label="közepes testű" data-value="közepes testű" selector>
            <input type="checkbox" data-label="nagytestű" data-value="nagytestű" selector>
        </div>
        <div class="sidebar-box" data-type="ivar">
            <h5>Ivar</h5>
            <input type="checkbox" data-label="Szuka" data-value="szuka" selector>
            <input type="checkbox" data-label="Kan" data-value="kan" selector>
        </div>
        <div class="sidebar-box" data-type="ivaros">
            <h5>Ivarosság</h5>
            <input type="checkbox" data-label="Ivaros" data-value="ivaros" selector>
            <input type="checkbox" data-label="Ivartalan" data-value="ivartalan" selector>
        </div>
        <div class="sidebar-box" data-type="telephely"></div>
        <div class="sidebar-box" data-type="status">
            <h5>Státusz</h5>
            <select id="status" selector>
                <option value="false">Mind</option>
                <option value="Nálunk van">Nálunk van</option>
                <option value="Korházban">Korházban</option>
                <option value="Sérült">Sérült</option>
                <option value="Eltávozott">Eltávozott</option>
            </select>
        </div>
        <div class="sidebar-box" data-type="foglalt">
            <h5>Foglalt</h5>
            <select id="foglalt" selector>
                <option value="false">Mind</option>
                <option value="1">Igen</option>
                <option value="0">Nem</option>
            </select>
        </div>
        <div class="sidebar-box" data-type="visible">
            <h5>Látható weboldalon</h5>
            <select id="visible" selector>
                <option value="false">Mind</option>
                <option value="1">Igen</option>
                <option value="0">Nem</option>
            </select>
        </div>
    </div>
    
    <div class="mobile-buttons">
        <button class="button-s b-light-brown c-white" sidebar><i class="fa-solid fa-filter"></i>Szűrők</button>
    </div>
    
    <table id="kutyatabla" class="c-table start-align"></table>
    


<?php $html->close(); ?>