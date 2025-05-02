class siteControleller
{
    constructor()
    {
        this.api = new api('../php/api/data.api.php');
        
        this.getTelephelyek();
        this.checkPhone();
        this.checkPassword();
        this.checkPassword2();
        this.upload();
    }

    upload()
    {
        $("#uploadBtn").click(function()
        {
            const data = formController.getformdata('ujdolgozo');
            console.log(data);
            this.api.post('upload-worker',data,function(response)
            {
                response.status == true && popup.set('Sikeres feltötlés','success');
            });
        }.bind(this));
    }

    checkPassword2()
    {
        $(document).on('keyup','#password2',function(e)
        {
            let password = $(e.target).val();
            let password2 = $("#password").val();
            password === password2 ? clearStatus($(e.target)) : setError($(e.target));
        });
    }

    checkPassword()
    {
        $(document).on('keyup','#password',function(e)
        {
            let password = $(e.target).val();
            const password_regex = /^(?=.*[A-Za-z])(?=.*\d).{8,}$/;
            password_regex.test(password) ? clearStatus($(e.target)) : setError($(e.target));
        });
    }

    checkPhone()
    {
        $(document).on('keyup','#telefonszam',function(e)
        {
            let telefonszam = $(e.target).val();
            const input = $(e.target);

            this.api.post('check-phone',{telefonszam:telefonszam},function(response)
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

    getTelephelyek(dolgozotelephely='')    
    {
        let html = ``;
        this.api.get('get-telephely',{},function(response)
        {
            const telephelyek = response.data.telephelyek;

            telephelyek.forEach((telephely) =>
            {
                let selected = telephely.nev == dolgozotelephely ? 'selected' : '';
                html += `<option value="${telephely.nev}" ${selected}>${telephely.nev}</option>`;
            });

            $("#telephely").html(html);
        });
    }

}


new siteControleller();