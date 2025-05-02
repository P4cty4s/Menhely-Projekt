class sitecontroller
{

    constructor()
    {
        this.api = new api("../php/api/kutyak.api.php");

        this.drawTable();
        this.handleSelector();
        this.deleteDog();
    }

    deleteDog()
    {
        const _this = this;
        $(document).on('click','.delete-dog',function(e)
        {
            let data =
            {
                kutyaid : $(e.target).data('id')
            }

            _this.api.delete('delete-dog',data,function(response)
            {
                _this.drawTable();
            });

        });
    }

    drawTable(selector=[])
    {
        selector.hasoffset = false;
        selector.needimg = false;
        selector.needKennel = true;

        this.api.get("get-dogs", selector, (response) =>
        {
            $("#kutyatabla").empty();
            const kutyak = response.data.kutyak;
            
            let head = `
                <thead>
                    <tr>
                        <th scope="col">Reg. szám</th>
                        <th scope="col">Név</th>
                        <th scope="col">Chip</th>
                        <th scope="col">Ivar</th>
                        <th scope="col">Méret</th>
                        <th scope="col">Születés</th>
                        <th scope="col">Bekerülés</th>
                        <th scope="col">Ivarosság</th>
                        <th scope="col">Telephely</th>
                        <th scope="col">Fogalt</th>
                        <th scope="col">Kennelszám</th>
                        <th scope="col">Státusz</th>
                        <th scope="col">Látható</th>
                        <th scope="col">Műveletek</th>
                    </tr>
                </thead>
            `;
            $("#kutyatabla").append(head);

            kutyak.forEach((kutya) =>
            {
                let html = `
                    <tr>
                        <td data-label="Regisztrációs szám"><div class="cell-wrap">${kutya.regszam}</div></td>
                        <td data-label="Név"><div class="cell-wrap">${kutya.nev}</div></td>
                        <td data-label="Chipszám"><div class="cell-wrap">${kutya.chipszam}</div></td>
                        <td data-label="Ivar"><div class="cell-wrap">${kutya.ivar}</div></td>
                        <td data-label="Méret"><div class="cell-wrap">${kutya.meret}</div></td>
                        <td data-label="Születés"><div class="cell-wrap">${kutya.szuletes}</div></td>
                        <td data-label="Bekerülés"><div class="cell-wrap">${kutya.bekerules}</div></td>
                        <td data-label="Ivarosság"><div class="cell-wrap">${kutya.ivaros}</div></td>
                        <td data-label="Telephely"><div class="cell-wrap">${kutya.telephely}</div></td>
                        <td data-label="Fogalt"><div class="cell-wrap">${kutya.foglalt ? "Igen" : "Nem"}</div></td>
                        <td data-label="Kennelszám"><div class="cell-wrap">${kutya.kennelnev}</div></td>
                        <td data-label="Státusz"><div class="cell-wrap">${kutya.status}</div></td>
                        <td data-label="Látható"><div class="cell-wrap">${kutya.visible ? "Igen" : "Nem"}</div></td>
                        <td data-label="Műveletek" class="muveletek">


                            <a href="kutyamodositas.php?id=${kutya.id}" class="desktop-button"><i class="fa-solid fa-gears desktop-button"></i></a>
                            <i class="fa-solid fa-trash-can c-error desktop-button delete-dog"data-id="${kutya.id}"></i>

                            <a href="kutyamodositas.php?id=${kutya.id}" class="mobile-button"><button><i class="fa-solid fa-gears mobile-button"></i> módosítás</button></a>
                            <button class="mobile-button delete-dog" data-id="${kutya.id}"><i class="fa-solid fa-trash-can c-error"></i> törlés</button>

                        </td>
                    </tr>
                `;

                $("#kutyatabla").append(html);
            });

        });
    }


    handleSelector()
    {
        $(document).on('change keyup', 'input[selector], .kor-range input, #selector-nev, select', function() {
            let filters = this.getSelectors();
            this.drawTable(filters);
        }.bind(this));
    }

    getSelectors()
    {
        let filters = {}; 
        
        $(".sidebar div[data-type]").each(function()
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
                
            $(this).find("input[type='text'], select").each(function()
            {
                values.push($(this).val().trim());
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

new sitecontroller();