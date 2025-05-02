class siteController
{
    constructor()
    {
        this.api = new api("../php/api/data.api.php");

        this.checkEmail();
        this.upload();
    }

    upload()
    {
        $("#uploadBtn").click(function()
        {
            const data = formController.getformdata('ujtelephely');
            this.api.post('upload-telephely',data,function(response)
            {
                popup.set('Telephely feltöltve!','success');
            });

        }.bind(this))
    }

    checkEmail()
    {
        $("#email").keyup(function(e)
        {
            let email = $(e.target).val();
            this.api.post('check-email',{email:email},function(response)
            {
                response.status == false ? setError($("#email")) : clearStatus("#email");
            });

        }.bind(this));
    }

    checkPhone()
    {
        const onlynum = /^[0-9]+$/;
        $("#telefonszam").keyup(function()
        {
            let phone = $(this).val();
            onlynum.test(phone) == false ? setError($(this)) : clearStatus($(this));
            console.log('a');
        });
    }

}

new siteController();