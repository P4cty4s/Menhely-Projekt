class siteController
{
    constructor()
    {
        this.api = new api('../php/api/user.api.php');
        this.handleLoginBtn();   
    }

    handleLoginBtn()
    {
        $("#loginBtn").click(function()
        {
            let data = formController.getformdata('login');
            this.api.post('check-login',data,function(response)
            {
                if (response.status == false)
                {
                    popup.set('Hibás felhasználónév vagy jelszó!','error');
                }
                else
                {
                    locate('kutyak.php');
                }

            }); 
        }.bind(this));
    }

}

new siteController();