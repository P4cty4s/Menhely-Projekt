class sitecontroller
{

    constructor()
    {
        this.dataapi = new api("../php/api/data.api.php");
        this.kutyaapi = new api("../php/api/kutyak.api.php");

        this.getImageRoot();
        this.getTelephelyek();
        this.checkChipszam();
        this.uploadBtn();
        this.modifyBtn();
        this.deleteImageBtn();
        this.switchtoindexBtn();

        this.getDogData();
    }   

    getImageRoot()
    {
        const dataapi = new api('../php/api/data.api.php');
        dataapi.get('get-imageroot',{},function(response)
        {
            const root = response.data.root;
            sessionStorage.setItem('imageroot',root);
        });
    }

    switchtoindexBtn()
    {
        $(document).on('click','.switch-to-index',function(e)
        {
            const _this = this;
            const kepid = $(e.target).data('kepid');
            const kutyaid = $(e.target).data('kutyaid');
            this.kutyaapi.put('switch-index',{kepid:kepid,kutyaid:kutyaid},function(response)
            {   
                if (response.status == true)
                {
                    popup.set('Indexkép megváltoztatva!','success');
                    _this.getDogData();
                }
            });
        }.bind(this));
    }

    deleteImageBtn()
    {
        $(document).on('click','.delete-img',function(e)
        {
            const _this = this;
            const kepid = $(e.target).data('id');
            this.kutyaapi.delete('delete-img',{id:kepid},function(response)
            {
                if (response.status = true)
                {
                    popup.set('Kép törölve!','success');
                    _this.getDogData();
                }
            });
        }.bind(this));
    }

    modifyBtn()
    {
        $("#modifyBtn").click(function()
        {
            const _this = this;
            let formdata = new FormData();
            
            // data

                // képek
                let images = $("#kepek")[0].files;
                for (let i = 0; i < images.length; i++)
                {
                    formdata.append("images[]", images[i]);
                }

                // egyéb
                let datas = formController.getformdata('modify');
                datas.id = GET('id');
                formdata.append('kutya-adat',JSON.stringify(datas));
                formdata.append('action','modify-dog');
                console.log(formdata);

            // ajax
            $.ajax
            ({
                url: '../php/api/kutyak.api.php',
                type: 'POST',
                data: formdata,
                contentType: false,
                processData: false,
                success: function(response)
                {
                    popup.set('Sikeres adatmódosítás!','success');
                    _this.getDogData();
                }
            });

        }.bind(this));
    }

    getDogData()
    {
        const kutyaid = GET('id');

        if (kutyaid !== null)
        {
            const params =
            {
                hasoffset : false,
                needimg : true,
                id : kutyaid
            };
            this.kutyaapi.get('get-dogs',params, (response) => 
            {
                isDataExist(response.data.kutyak);
                let kutya = response.data.kutyak[0];
                
                for (let key in kutya)
                {
                    let input = $(`#${key}`);
                    if (input.length && input.attr('type') !== 'file')
                    {
                        input.val(kutya[key]);
                        //console.log(key+" = "+kutya[key]);
                    }

                }
                $(".form-title").html(`${kutya.nev} adatai`);
                this.getTelephelyek(kutya.telephely);
                let form = $("#modify");
                formController.handleTriggerButton(form);

                // képek
                let html = ``;
                const imgsource = sessionStorage.getItem('imageroot');

                if (kutya.indexkep != false)
                {
                    html += `
                        <div class="col-6 col-md-4 col-lg-3 is-invalid">
                            <img src="${imgsource}${kutya.indexkep.nev}" class="img-fluid gallery-img"></img>
                            <div class="image-info">
                                <button class="b-error c-white delete-img" data-id="${kutya.indexkep.id}">Törlés</button>
                            </div>
                        </div>
                    `;

                }

                kutya.kepek.forEach((kep) =>
                {
                    html += `
                        <div class="col-6 col-md-4 col-lg-3">
                            <img src="${imgsource}${kep.nev}" class="img-fluid gallery-img"></img>
                            <div class="image-info">
                                <button class="b-light-gray switch-to-index" data-kepid=${kep.id} data-kutyaid="${kutya.id}">Beállítás indexképnek</button>
                                <button class="b-error c-white delete-img" data-id="${kep.id}">Törlés</button>
                            </div>
                        </div>
                    `;

                    console.log('image-source => ',`${imgsource}${kep.nev}`);

                });

                $(".images").html(html);
            });
        }
    }

    uploadBtn()
    {
        $("#uploadBtn").click(function()
        {
            let formdata = new FormData();
            
            // data

                // képek
                let images = $("#kepek")[0].files;
                for (let i = 0; i < images.length; i++)
                {
                    formdata.append("images[]", images[i]);
                }

                // egyéb
                let datas = formController.getformdata('upload');
                formdata.append('kutya-adat',JSON.stringify(datas));
                formdata.append('action','upload-dog');
                console.log(formdata);

            // ajax
            $.ajax
            ({
                url: '../php/api/kutyak.api.php',
                type: 'POST',
                data: formdata,
                contentType: false,
                processData: false,
                success: function(response)
                {
                    const kutyaid = response.data.kutyaid;
                    locate(`kutyamodositas.php?id=${kutyaid}`);
                }
            });

        });
    }

    checkChipszam()
    {
        $("#chipszam").on('keyup change',function(e)
        {
            let data = { chipszam : $(e.target).val() };
            this.kutyaapi.post('check-chipszam',data,function(response)
            {
                response.status == false ? setError($(e.target)) : clearStatus($(e.target));
            });
        }.bind(this));
    }

    getTelephelyek(kutyatelehpely)    
    {
        let html = ``;
        this.dataapi.get('get-telephely',{},function(response)
        {
            const telephelyek = response.data.telephelyek;

            telephelyek.forEach((telephely) =>
            {
                let selected = telephely.nev == kutyatelehpely ? 'selected' : '';
                html += `<option value="${telephely.nev}" ${selected}>${telephely.nev}</option>`;
            });

            $("#telephely").html(html);
        });
    }


}   

new sitecontroller();