class siteController
{
    constructor()
    {
        this.api = new api('../php/api/kutyak.api.php');
        this.dataapi = new api('../php/api/data.api.php');
        
        this.getImageRoot();
        this.getData();
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

    getData()
    {
        const _this = this;
        const data =
        {
            hasoffset : false,
            needimg : true,
            id : GET('id')
        };
        this.api.get('get-dogs',data,function(response)
        {
            if (response.data.kutyak == undefined)
            {
                locate('error.php');
            }
            const kutya = response.data.kutyak[0];

            const source = sessionStorage.getItem('imageroot');
            const imageName = kutya.indexkep.nev == undefined ? 'noimage.png' : kutya.indexkep.nev;
            console.log('image =>',imageName);
            
            // kutya kép
            const kutyakep = $(".kutya-kep");
            const image =  $(`<img src="${source}${imageName}">`);
            
            kutyakep.append(image);

            // kutya adatlap
            const datas = Array.from($('[data]'));
            console.log(datas);
            datas.forEach((data) =>
            {
                let type = $(data).attr('type');
                $(data).html(kutya[type]);
            });

            // galéria
            const kepek = kutya.kepek;
            const galeria = $(".gallery");

            kepek.forEach((kep) =>
            {
                let img = $(`
                        <div class="gallery-item">
                            <div class="rope"></div>
                            <img src="${source}${kep.nev}" class="col-12 col-md-6 col-lg-6 col-xl-4">
                        </div>
                    `);
                galeria.append(img);
            });
        });



    }

}

new siteController();