<?php

    require_once "../config.php";
    require HTML;

    $html = new html('HIBA - 404');
    $html->setFooter(true);
    $html->render();
?>
<style>

    body
    {
        background-color: var(--image-bg);
    }

    #content
    {
        display: flex;
        justify-content: center;
        align-items: center;
    }

    img
    {
        max-width: 450px;
        max-height: 450px;
    }

</style>

    <img src="../src/images/project/notfound.png">


<?php $html->close(); ?>