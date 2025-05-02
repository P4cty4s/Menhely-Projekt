class siteController
{
    constructor()
    {
        this.api = new api('../php/api/kutyak.api.php');

        this.getImageRoot();
        this.drawGallery();
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

    drawGallery()
    {
        this.api.get('get-images',{},function(response)
        {
            const images = response.data.images;
            const source = sessionStorage.getItem('imageroot');
            const gallery = $(".galeria");

            images.forEach((item) =>
            {
                let section = ``;

                section += `<div>`;
                section += `<div class="kutyanev"><i class="fa-solid fa-paw"></i><h3>${item.kutya}</h3></div>`;
                section += `<div class="gallery">`;
                item.kepek.forEach((img) =>
                {
                    section += `<div class="gallery-item">`;
                    section += `<div class="rope"></div>`;
                    section += `<img src="${source}${img.nev}">`;
                    section += `</div>`;
                });
                section += `</div>`;
                section += `</div>`;

                gallery.append(section);
            }); 

        });
    }

}

new siteController();