class siteControleller
{
    constructor()
    {
        this.userapi = new api('../php/api/user.api.php');
        this.dataapi = new api('../php/api/data.api.php');

        this.getData();
        this.checkPhone();
        this.modifyBtn();
    }

    modifyBtn()
    {
        const _this = this;
        $(document).on('click','#uploadBtn',function(e)
        {
            const data = formController.getformdata('modify-dolgozo');
            data.id = GET('id');
            _this.dataapi.put('modify-user',data,function(response)
            {
                popup.set('Sikeres adatmódosítás','success');
            });
            

        });
    }

    checkPhone()
    {
        $(document).on('keyup','#telefonszam',function(e)
        {
            let telefonszam = $(e.target).val();
            const input = $(e.target);

            this.dataapi.post('check-phone',{telefonszam:telefonszam},function(response)
            {
                if (response.status == false)
                {
                    setError(input);
                }

                if (response.data.msg)
                {
                    popup.set(response.data.msg,'error');
                }
                else
                {
                    popup.clear();
                }

            });

        }.bind(this));
    }

    getData()
    {
        const _this = this;
        this.userapi.get('get-user',{id:GET('id')},function(response)
        {
            isDataExist(response.data.user);
            const worker = response.data.user;
            
            for (let key in worker)
            {
                $(`#${key}`).val(worker[key]);
                console.log(key);   
            }

            _this.getTelephelyek(worker.telephely);
            formController.handleTriggerButton();
        });
    }

    getTelephelyek(dolgozotelephely)
    {
        this.dataapi.get('get-telephely',{},function(response)
        {
            const telephelyek = response.data.telephelyek;
            let html = ``;

            telephelyek.forEach((telephely) =>
            {
                let selected = telephely.nev == dolgozotelephely ? 'selected' : '';
                html += `<option value="${telephely.nev}" ${selected}>${telephely.nev}</option> `;
            });
            $("#telephely").html(html);
        });
    }

}

new siteControleller();