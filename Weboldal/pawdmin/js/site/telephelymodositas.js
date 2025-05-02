class siteControleller
{
    constructor()
    {
        this.api = new api('../php/api/data.api.php');
        this.getData();
        this.modify();
    }

    modify()
    {
        const _this = this;
        $(document).on('click','#uploadBtn',function(e)
        {
            const data = formController.getformdata('telephely');
            data.id = GET('id');
            _this.api.put('modify-telephely',data,function(response)
            {
                popup.set('Sikeres adatmódosítás','success');
                _this.getData();
            });

        });
    }

    getData()
    {
        const _this = this;
        let data = { telephelyid : GET('id') };

        this.api.get('get-telephely-id',data,function(response)
        {
            isDataExist(response.data.telephely);
            const datas = Array.from($("input[req]"));
            const telephely = response.data.telephely;

            datas.forEach((data) =>
            {
                console.log(data)
                let type = $(data).attr('id');
                $(`#${type}`).val(telephely[type]);
            });

        });

    }

}

new siteControleller();