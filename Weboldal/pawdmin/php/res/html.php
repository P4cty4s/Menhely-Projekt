<?php

    require_once "../config.php";
    require_once DATABASE;

    class html
    {
        private string $title;
        private array $css;
        private array $js;
        private bool $autoreload;
        private bool $onlylogged;
        private $hero;
        private bool $hasFooter;

        public function __construct(string $title)
        {
            global $import_css, $import_js;

            $this->title = $title;
            $this->css = $import_css;
            $this->js = $import_js;
            $this->autoreload = false;
            $this->onlylogged = false;
            $this->hero = false;
            $this->hasFooter = false;
        }   

        // controller
        private function getactivesite()
        {
            $result = basename($_SERVER['PHP_SELF']);
            return  $result == '' ? 'index.php' : $result;
        }

        public function navigateifnotlogged(string $root)
        {
            if ($this->onlylogged && !isset($_COOKIE['userid']))
            {
                header("Location: $root");
            }
        }

        private function getData()
        {
            $result = db::get('SELECT * FROM menhely');
            return $result;
        }

        // view
        public function render()
        {
            $this->renderhead();
            $this->renderbody();
        }

        public function close()
        {
            echo '      </div>';
            $this->hasFooter && $this->renderFooter();
            echo '  </body>';
            echo '</html>';
        }

            // footer
            private function renderFooter()
            {

                $szervezet = $this->getData();
                global $navigation;
                global $siteName;

                echo '<footer class="text-white pt-5 pb-4">';
                echo '  <div class="container text-center text-md-start">';
                echo '    <div class="row">';

                echo '      <div class="col-md-3 col-lg-3 col-xl-3 mx-auto mb-4">';
                echo '        <h5 class="text-uppercase fw-bold">'.$szervezet['nev'].'</h5>';
                echo '        <p>All in one megoldás kutyamenhelyek számára</p>';
                echo '      </div>';

                echo '      <div class="col-md-2 col-lg-2 col-xl-2 mx-auto mb-4">';
                echo '        <h6 class="text-uppercase fw-bold">Navigáció</h6>';
                echo '        <ul class="list-unstyled">';
                foreach($navigation as $nav)
                {
                    echo '  <li><a href="'.$nav['href'].'" class="text-white text-decoration-none">'.$nav['title'].'</a></li>';
                }
                echo '        </ul>';
                echo '      </div>';

                echo '      <div class="col-md-4 col-lg-3 col-xl-3 mx-auto mb-md-0 mb-4">';
                echo '        <h6 class="text-uppercase fw-bold">Follow us</h6>';
                
                //ikonok
                echo '<a href="#" class="me-2 d-inline-block text-center" style="background-color: white; border-radius: 50%; width: 40px; height: 40px; line-height: 40px;" aria-label="Facebook">';
                echo '  <i class="fab fa-facebook-f" style="color: #1877F2; font-size: 20px;"></i>';
                echo '</a>';

                echo '<a href="#" class="me-2 d-inline-block text-center" style="background-color: white; border-radius: 50%; width: 40px; height: 40px; line-height: 40px;" aria-label="TikTok">';
                echo '  <i class="fab fa-tiktok" style="color: #000000; font-size: 20px;"></i>';
                echo '</a>';

                echo '<a href="#" class="me-2 d-inline-block text-center" style="background-color: white; border-radius: 50%; width: 40px; height: 40px; line-height: 40px;" aria-label="Instagram">';
                echo '  <i class="fab fa-instagram" style="color: #E1306C; font-size: 20px;"></i>';
                echo '</a>';

                echo '<a href="#" class="d-inline-block text-center" style="background-color: white; border-radius: 50%; width: 40px; height: 40px; line-height: 40px;" aria-label="YouTube">';
                echo '  <i class="fab fa-youtube" style="color: #FF0000; font-size: 20px;"></i>';
                echo '</a>';

                echo '      </div>';

                echo '    </div>';
                echo '  </div>';

                echo '  <div class="text-center mt-4 border-top pt-3">';
                echo '    <small>© 2025 '.$siteName.'. Minden jog fenntartva.</small>';
                echo '  </div>';
                echo '</footer>';
            }

            // body
            private function renderbody()
            {
                $this->renderheader();
                $this->rendermobilenav();
                echo '<body>';
                $this->hero != false && $this->renderHero();
                echo '  <div id="content">';
            }

            private function renderHero()
            {
                $hero = $this->hero;
                global $imageRoot_project;

                echo '<div class="hero slide-down" style="background-image:url('.$imageRoot_project.''.$hero['bg'].')">';
                echo '  <div class="container">';
                echo '      <h1>'.$hero['title'].'</h1>';
                echo '      <p>'.$hero['altitle'].'</p>';
                if ($hero['button'] != false)
                {
                    $button = $hero['button'];
                    echo '<button><a href="'.$button['href'].'">'.$button['text'].'</a></button>';
                }
                echo '  </div>';
                echo '</div>';
            }

            private function renderheader()
            {
                $szervezet = $this->getData();
                global $siteName;
                global $imageRoot_project;

                $logo = '<div id="site-title"><img src="'.$imageRoot_project.'logo.png" class="logo"><span class="title">'.$szervezet['nev'].'</span></div>';

                echo '<header>';

                    //mobile
                    echo '<div class="mobile">';
                    echo '  <div id="site-title"><img src="'.$imageRoot_project.'logo.png" class="logo"><span class="title">'.$szervezet['nev'].'</span></div>';
                    echo '  <i class="fa-solid fa-bars-staggered" id="mobile-menu-btn"></i>';
                    //$this->rendermobilenav();
                    echo '</div>';

                    //desktop
                    echo '<div class="desktop">';
                    echo    $logo;
                    echo '  <nav>';
                                $this->rendernav();
                    echo '  </nav>';
                    echo '</div>';

                echo '</header>';
            }

            private function rendermobilenav()
            {
                $szervezet = $this->getData();                

                echo '<div class="mobile-nav slide-down">';
                echo '  <h3>'.$szervezet['nev'].'</h3>';
                echo '  <nav>';
                        $this->rendernav();
                echo '  </nav>';
                echo '</div>';
            }

            private function rendernav()
            {
                global $navigation;

                $activesite = $this->getactivesite();
                foreach($navigation as $a)
                {
                    $active = $a['href'] == $activesite ? 'active' : '';
                    echo '<a href="'.$a['href'].'" class="'.$active.'">'.$a['icon'].''.$a['title'].'</a>';
                }
            }

            // head
            private function renderhead()
            {
                global $lang, $db_charset;

                echo '<!DOCTYPE html>';
                echo '<html lang="'.$lang.'">';
                echo '  <head>';
                echo '      <meta charset="'.$db_charset.'">';
                echo '      <meta name="viewport" content="width=device-width, initial-scale=1.0">';
                echo $this->autoreload ? '<meta http-equiv="refresh" content="2">' : '';
                            $this->rendercss();
                            $this->renderjs();
                echo '      <title>'.$this->title.'</title>';
                echo '  </head>';
            }

            private function rendercss()
            {
                foreach($this->css as $css)
                {
                    echo '<link rel="stylesheet" href="'.$css.'">';
                }
            }

            private function renderjs()
            {
                foreach($this->js as $js)
                {
                    echo '<script src="'.$js.'" defer></script>';
                }
            }

        // setters
        public function settitle(string $title) { $this->title = $title; }
        public function addcss(string $root) { $this->css[] = $root; }
        public function addjs(string $root) { $this->js[] = $root; }
        public function setautoreload(bool $value) { $this->autoreload = $value; }
        public function setonlylogged(bool $value) { $this->onlylogged = $value; }
        public function setFooter(bool $value) { $this->hasFooter = $value; }
        public function setHero(string $title, string $altitle, string $bg, $button=false)
        {
            $this->hero = ['title' => $title, 'altitle' => $altitle, 'bg' => $bg, 'button' => $button];
        }
    }




?>