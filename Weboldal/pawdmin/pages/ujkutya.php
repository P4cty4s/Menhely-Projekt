<?php

    require_once "../config.php";
    require HTML;

    $html = new html('Új kutya hozzáadása');
    $html->setonlylogged(true);
    $html->navigateifnotlogged('login.php');

    // import js
    $html->addjs(JS_SITE."kutya-adat.js");

    // import css

    $html->render();
?>

    <div class="container contentMarginTop">
        <form id="upload">
            <h3 class="form-title">Kutya hozzáadása</h3>
            <input id="nev" type="text" data-label="Név" req>
            <input id="chipszam" type="text" data-label="Chipszám" placeholder="15 karakter hosszú számsorozat" req>
            <input id="regszam" type="text" data-label="Regisztrációs szám" req>
            <input id="szuletes" type="date" data-label="Születés" req>
            <input id="bekerules" type="date" data-label="Bekerülés" req>
            <select id="ivar" data-label="Ivar">
                <option value="szuka">Szuka</option>
                <option value="kan">Kan</option>
            </select>
            <select id="meret" data-label="Méret">
                <option value="kölyök">Kölyök</option>
                <option value="kistestű">Kistestű</option>
                <option value="közepes testű">Közepes testű</option>
                <option value="nagytestű">Nagytestű</option>
            </select>
            <select id="ivaros" data-label="Ivarosság">
                <option value="ivaros">Ivaros</option>
                <option value="ivartalan">Ivartalan</option>
            </select>
            <select id="telephely" data-label="Telephely"></select>
            <select id="status" data-label="Státusz">
                <option value="Nálunk van">Nálunk van</option>
                <option value="Korházban">Korházban</option>
                <option value="Sérült">Sérült</option>
                <option value="Eltávozott">Eltávozott</option>
            </select>
            <select id="visible" data-label="Látható weboldalon">
                <option value="1">Igen</option>
                <option value="0">Nem</option>
            </select>
            <input id="kepek" type="file" data-label="Képek felöltése" multiple>
            <button id="uploadBtn" trigger>Feltöltés</button>
        </form>
    </div>


<?php $html->close(); ?>