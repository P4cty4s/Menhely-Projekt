class customButton extends HTMLElement
{
    constructor()
    {
        super();
        this.type = $(this).attr('type');
        this.setHTML();
    }

    setHTML()
    {
        var icon;
        switch(this.type)
        {
            case 'delete': icon = `<i class="fa-solid fa-trash"></i>`; break;
        }

        this.setButton(icon);

    }

    setButton(icon)
    {
        const text = customButton.getText($(this));
        const html = customButton.setText(icon,text);
        customButton.apply($(this),html);
    }

    /* tools */
    static setText(icon,text) { return `${icon} <span>${text}</span>`; }
    static getText($button) { return $button.text(); }
    static apply($button,html) { $button.html(html); }

}

customElements.define('c-button',customButton);