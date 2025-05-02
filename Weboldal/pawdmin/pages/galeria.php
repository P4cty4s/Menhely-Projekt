<?php

    require_once "../config.php";
    require_once HTML;

    $html = new html('Galéria');

    // site settings
    $html->setHero('Galéria','Kutyáink képekben','galeria.png');
    $html->setFooter(true);

    // import css
    $html->addcss(CSS_ELEMENT."gallery.css");
    $html->addcss(CSS_SITE."galeria.css");

    // import js
    $html->addjs(JS_SITE."gallery.js");

    $html->render();
?>

    <div class="galeria"></div>


<?php $html->close(); ?>