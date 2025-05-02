class SiteController
{
    constructor()
    {
        this.api = new api("../php/api/kutyak.api.php");

        this.getImageRoot();
        this.drawDogs();
        this.drawPagination();
        this.turnoffcheckboxes();
        this.handleSelector();
        this.handleSwitchPage();
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

    handleSwitchPage()
    {
        $(document).on('click','#next-page, #next-page i',function(e)
        {
            let page = $(e.target).data('page');
            let filters = this.getSelectors();
            this.drawDogs(filters,page);
            this.drawPagination(filters,page);

        }.bind(this));

        $(document).on('click','#prev-page',function(e)
        {
            let page = $(e.target).data('page');
            let filters = this.getSelectors();
            this.drawDogs(filters,page);
            this.drawPagination(filters,page);
        }.bind(this));

    }

    drawPagination(selector=[],recentpage=0)
    {
        this.api.get("get-dogs-count", selector, (response) =>
        {
            let count = response.data.count;
            let pagecount = Math.ceil(count/20);
            let html = ``;

            if (recentpage > 0)
            {
                html += `<button id="prev-page" data-page="${recentpage-1}"><i data-page="${recentpage-1}" class="fa-solid fa-arrow-left"></i></button>`;
            }
            html += ` <span>${recentpage+1}/${pagecount}</span>`;
            if (recentpage < pagecount-1)
            {
                html += `<button id="next-page" data-page="${recentpage+1}"><i data-page="${recentpage+1}" class="fa-solid fa-arrow-right-long"></i></button>`;
            }

            $(".pagination").html(html);
        });
    }

    drawDogs(selector=[],page=0)
    {
        let offset = page * 20;
        selector.offset = offset;
        selector.hasoffset = false;
        selector.needimg = true;
        selector.visible = 1

        this.api.get("get-dogs", selector, (response) =>
        {
            $("#kutyak").empty();

            const kutyak = response.data.kutyak;

            kutyak.forEach((kutya) =>
            {
                let container = $('<div class="col-12 col-md-6 col-lg-6 col-xl-4 fade-in"></div>');
                let source = sessionStorage.getItem('imageroot');
                let img = kutya.indexkep != false ? kutya.indexkep.nev : 'noimage.png';
                let foglalt = kutya.foglalt == 1 ? '<span class="c-error"> - foglalt!</span>' : '';
                let genderSymbol = kutya.ivar == 'kan' ? '<i class="fa-solid fa-mars-stroke"></i>' : '<i class="fa-solid fa-venus"></i>';

                let html = `
                    <div class="box kutya grow-on-hover">      
                        <div class="image" style="background-image:url('${source}${img}')"></div>
                        <div class="info">
                            <h4>${kutya.nev} ${foglalt}</h4>
                            <label>${genderSymbol} ${kutya.ivar}</label>
                            <div class="tags">
                                <div class="tag">${kutya.kor} éves</div>
                                <div class="tag">${kutya.meret}</div>
                                <div class="tag">${kutya.telephely}</div>
                                <div class="tag">${kutya.ivaros}</div>
                            </div>
                            <a href="kutya-adatlap.php?id=${kutya.id}">
                                <button class="b-ligh-gray c-black">Megtekintés</button>
                            </a>
                        </div>
                `;
                console.log('kép root ',`${source}${img}`);
                html += `</div>`;

                container.html(html);
                $("#kutyak").append(container);
            });
        });
    }

    /*
    
        <div class="info">
            <h4 class="c-white">${kutya.nev}</h4>
                
        </div>
    
    */

    turnoffcheckboxes()
    {
        $(".sidebar input[type='checkbox']").prop("checked",false);
    }

    handleSelector()
    {
        $(document).on('change keyup', 'input[selector], .kor-range input, #selector-nev,#mobil-nev , select', function(e)
        {
            let filters = this.getSelectors();
            this.drawDogs(filters);
            this.drawPagination(filters);
        }.bind(this));
    }

    getSelectors()
    {
        let filters = {}; 
        
        $("div[data-type]").each(function()
        {
            let key = $(this).data("type"); 
            let values = []; 
        
            $(this).find("input[type='checkbox']:checked").each(function()
            {
                values.push($(this).data("value")); 
            });
        
            $(this).find("input[type='number']").each(function()
            {
                values.push($(this).val()); 
            });
                
            $(this).find("input[type='text']:visible, select").each(function()
            {
                values.push($(this).val());
                console.log($(this).val());
            });

            if (values.length > 0)
            {
                filters[key] = values;
            }
        });
        console.log(filters);
        return filters;
    }

}

new SiteController();
