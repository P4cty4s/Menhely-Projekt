class sidebarController
{
    constructor()
    {
        this.dataapi = new api('../php/api/data.api.php');
        this.kutyaapi = new api('../php/api/kutyak.api.php');

        this.open();
        this.close();
        this.fixNotAppearInDesktop();

        this.getTelephelyek();
        this.getKorRange();

        getwindowwidth() < 768 && hideelement($(".sidebar"));
    }

    /* view */
    getKorRange()
    {
        this.kutyaapi.get('get-korrange',{},function(response)
        {
            const range = response.data.korrange;
            let html = `
                <h5>Kor</h5>
                <div class="kor-range">
                    <input type="number" id="min-kor" value="${range.fiatal}" data-value="${range.fiatal}" min="0" max="${range.idos}" selector>
                    <input type="number" id="max-kor" value="${range.idos}" data-value="${range.idos}" min="0" max="${range.idos}" selector>
                </div>
                <span id="rangeValue"></span>
            `;

            console.log(range.fiatal);

            $(".sidebar div[data-type='kor']").html(html);
        });
    }

    getTelephelyek()
    {
        this.dataapi.get('get-telephely',{},function(response)
        {
            let html = `<h5>Telephely</h5>`;
            const telephelyek = response.data.telephelyek;

            telephelyek.forEach((telephely) =>
            {
                html += `
                    <div class="input-container-checkbox">
                        <input type="checkbox" data-value="${telephely.nev}" selector>
                        <label>${telephely.nev}</label>
                    </div>
                `;
            });
            $(".sidebar div[data-type='telephely']").html(html);
        });
    }

    /* controller */
    fixNotAppearInDesktop()
    {
        $(window).on('resize',function()
        {
            if (getwindowwidth() >= 768 && $(".sidebar").hasClass('hidden'))
            {
                $(".sidebar").removeClass('hidden');
            }
        });
    }

    close()
    {
        $(document).click(function (event)
        {
            console.log('click a dokumentumra')
            if (getwindowwidth() <= 768)
            {
                if (!$(event.target).closest(".sidebar, button[sidebar]").length)
                {
                    console.log('bezártás');
                    $(".darkLayer").remove();
                    hideelement($(".sidebar"));
                }
            }
        });
        
    }

    open()
    {
        $("button[sidebar]").click(function()
        {
            toggleelement($(".sidebar"));
        });
    }

}

new sidebarController();