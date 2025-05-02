class popup
{

    constructor()
    {
        this.close();
    }

    static set(text,type)
    {
        let html = `

            <div class="popup-box ${type}-popup">
                <i class="fa-solid fa-circle-exclamation"></i>
                ${text}
                <i class="fa-solid fa-circle-xmark close-popup"></i>
            </div>
        `;

        $("body").append(html);
    }

    close()
    {
        $(document).on('click','.close-popup',function()
        {
            $(".popup-box").remove();
        });
    }

    static clear()
    {
        $(".popup-box").remove();
    }

}

new popup();
