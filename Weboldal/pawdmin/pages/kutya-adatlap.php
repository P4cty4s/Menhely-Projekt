<?php

    require_once "../config.php";
    require_once HTML;

    $html = new html('');

    // site
    $html->setHero('Vigyél haza!','Kérlek!','adatlap.png',['text' => "többi lakótársam",'href' => "kutyak.php"]);
    $html->setFooter(true);

    // import js
    $html->addjs(JS_SITE."kutya-adatlap.js");

    // import css
    $html->addcss(CSS_ELEMENT."gallery.css");
    $html->addcss(CSS_SITE."kutya-adatlap.css");
    $html->addcss(CSS_SITE."galeria.css");

    $html->render();
?>

    <div class="kutya-container contentMarginTop">

        <div class="kutya-kep gallery-item"></div>

        <div class="kutya-adatlap box">
            <h3 type="nev" class="form-title" data></h3>
            <table class="adatlap">
                <tr>
                    <td class="c-light-brown">Kutya neve</td>
                    <td type="nev" data></td>
                </tr>
                <tr>
                    <td class="c-light-brown">Kutya ivara</td>
                    <td type="ivar" data></td>
                </tr>
                <tr>
                    <td class="c-light-brown">Kutya Mérete</td>
                    <td type="meret" data></td>
                </tr>
                <tr>
                    <td class="c-light-brown">Született</td>
                    <td type="szuletes" data></td>
                </tr>
                <tr>
                    <td class="c-light-brown">Hozzánk került</td>
                    <td type="bekerules" data></td>
                </tr>
                <tr>
                    <td class="c-light-brown">Telephely, ahol lakik</td>
                    <td type="telephely" data></td>
                </tr>
                <tr>
                    <td class="c-light-brown">Ivarosság</td>
                    <td type="ivaros" data></td>
                </tr>
            </table>
        </div>

        
    </div>
    <div class="kutyanev"><i class="fa-solid fa-paw"></i><h2>Galéria</h2></div>
    <div class="gallery"></div>

<?php $html->close(); ?>