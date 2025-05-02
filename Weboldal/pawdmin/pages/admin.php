<?php

    require_once "../config.php";
    require HTML;

    $html = new html('Dolgozói felület');
    $html->setonlylogged(true);
    $html->navigateifnotlogged('login.php');
    
    // import js

    // import css
    $html->addcss(CSS_SITE."admin.css");

    $html->render();
?>
    
    <div class="container contentMarginTop">

        <h2>Kutyák</h2>
        <!-- kutyák -->
        <div class="row">

            <div class="col-12 col-md-6 col-lg-6 col-xl-3">
                <a href="kutyatabla.php">
                    <div class="box navigate-box" style="background-image: url(../src/images/project/tablazat.png)">
                        <div class="info">
                            <h4>Összes kutya táblázat</h4>
                        </div>
                    </div>
                </a>
            </div>

            <div class="col-12 col-md-6 col-lg-6 col-xl-3">
                <a href="ujkutya.php">
                    <div class="box navigate-box" style="background-image: url(../src/images/project/ujkutya.png)">
                        <div class="info">
                            <h4>Kutya hozzáadása</h4>
                        </div>
                    </div>
                </a>
            </div>

            <div class="col-12 col-md-6 col-lg-6 col-xl-3">
                <a href="udvarok.php">
                    <div class="box navigate-box" style="background-image: url(../src/images/project/udvarok.png)">
                        <div class="info">
                            <h4>Udvarok rendje</h4>
                        </div>
                    </div>
                </a>
            </div>

        </div>
        
        <!-- szervezeti beállítások -->
        <h2>Szervezeti beállítok</h2>
        <div class="row">

            <div class="col-12 col-md-6 col-lg-6 col-xl-3">
                <a href="dolgozok.php">
                    <div class="box navigate-box" style="background-image: url(../src/images/project/dolgozok.png)">
                        <div class="info">
                            <h4>Dolgozók</h4>
                        </div>
                    </div>
                </a>
            </div>

            <div class="col-12 col-md-6 col-lg-6 col-xl-3">
                <a href="telephelyek.php">
                    <div class="box navigate-box" style="background-image: url(../src/images/project/telephely.png)">
                        <div class="info">
                            <h4>Telephelyek</h4>
                        </div>
                    </div>
                </a>
            </div>

            <div class="col-12 col-md-6 col-lg-6 col-xl-3">
                <a href="beallitasok.php">
                    <div class="box navigate-box" style="background-image: url(../src/images/project/beallitasok.png)">
                        <div class="info">
                            <h4>Szervezeti beállítások</h4>
                        </div>
                    </div>
                </a>
            </div>

            <div class="col-12 col-md-6 col-lg-6 col-xl-3">
                <a href="changelog.php">
                    <div class="box navigate-box" style="background-image: url(../src/images/project/tablazat.png)">
                        <div class="info">
                            <h4>Előzmények</h4>
                        </div>
                    </div>
                </a>
            </div>

        </div>

    </div>


<?php $html->close(); ?>