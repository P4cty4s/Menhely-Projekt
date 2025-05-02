class siteController
{
    constructor()
    {
        this.api = new api("../php/api/data.api.php");

        this.getSzervezetAdatok();
        this.modify();
    }

    modify()
    {
        $("#szervezet-modify").click(function(e)
        {
            const _this = this;
            const data = formController.getformdata('szervezet');
            this.api.put('modify-szervezet',data,function(response)
            {
                _this.getSzervezetAdatok();
            });

        }.bind(this));
    }

    getSzervezetAdatok()
    {
        this.api.get('get-szervezet-adatok',{},function(response)
        {
            const szervezet = response.data.szervezet;

            for (let key in szervezet)
            {
                $(`#${key}`).val(szervezet[key]);
            }

        });
    }

}

new siteController();