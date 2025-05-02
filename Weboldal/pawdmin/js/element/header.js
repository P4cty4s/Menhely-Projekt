class headerController
{
    constructor()
    {
        this.handleMobilenavMenu();
    }

    handleMobilenavMenu()
    {
        $("#mobile-menu-btn").click(function()
        {
            $(".mobile-nav").toggle();
        });

        $("header").click(function(event)
        {
            if (!$(event.target).is("#mobile-menu-btn"))
            {
                $(".mobile-nav").hide();
            }
        });
        

    }

    static setHeaderTitle(text)
    {
        $("#site-title").html(text);
    }

}

new headerController();