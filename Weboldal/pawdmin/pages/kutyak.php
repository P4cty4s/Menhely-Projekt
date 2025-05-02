<?php
    require_once "../config.php";
    require HTML;

    $html = new html('Gazdára váró kutyák');

    // site settings
    $html->setHero('Örökbe fogadhatsz egy barátot','Adj egy kutyának második esélyt - szerezd meg életed társát!','orokbefogadas.png');
    $html->setFooter(true);

    // import css
    $html->addcss(CSS_SITE."kutyak.css");
    $html->addcss(CSS_ELEMENT."sidebar.css");

    // import js
    $html->addjs(JS_SITE."kutyak.js");
    $html->addjs(JS_ELEMENT."sidebar.js");

    $html->render();
?>

    <!-- SIDEBAR -->
    <div class="sidebar">
        <h2 class="sidebar-title">Kutyák szűrése</h2>
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
        <div class="sidebar-box desktop-nev" data-type="nev">
            <input type="text" placeholder="keresés névre" id="selector-nev" selector>
        </div>
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
        <div class="sidebar-box" data-type="foglalt">
            <input type="checkbox" data-value="0" data-label="Foglalt kutyák eltüntetése" selector>
        </div>
    </div>


    <div>
        <div class="content">

            <div class="mobile-buttons">
                <div data-type="nev">
                    <input id="mobil-nev" type="text" placeholder="keresés névre" selector mobile-name-selector>
                </div>
                <button class="button-s b-light-brown c-white" id="szures" sidebar><i class="fa-solid fa-filter"></i> szűrés</button>
            </div>
        </div>
        
        <div class="pagination"></div>
        <div id="selector-tags"></div>
        <div class="container">
            <div class="row" id="kutyak"></div>
        </div>

        <div class="pagination"></div>

    </div>


<?php $html->close(); ?>