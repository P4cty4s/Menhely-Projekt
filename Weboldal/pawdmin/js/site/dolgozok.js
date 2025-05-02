class siteControleller
{
    constructor()
    {
        this.api = new api("../php/api/data.api.php");
        
        this.drawTable();
        this.delete();
    }

    delete()
    {
        $(document).on('click','.delete',function(e)
        {
            let id = $(e.target).data('id');
            let _this = this;

            this.api.delete('delete-worker',{id:id},function(response)
            {
                response.status == true && _this.drawTable();
            });

        }.bind(this));
    }

    drawTable()
    {
        this.api.get('get-workers',{},function(response)
        {
            const workers = response.data.workers;
            let html = ``;

            html += `
                <thead>
                    <tr>
                        <td scope="col">Azonosító</td>
                        <td scope="col">Felhasználónév</td>
                        <td scope="col">Telefonszám</td>
                        <td scope="col">Telephely</td>
                        <td scope="col">Jogosultság</td>
                        <td scope="col">Műveletek</td>
                    </tr>
                </thead>

            `

            workers.forEach((worker) =>
            {
                let jogosultsag = worker.admin == 0 ? 'dolgozó' : 'admin';

                html += `

                    <tr>
                        <td data-label="Azonosító">${worker.id}</td>
                        <td data-label="Felhasználónév">${worker.username}</td>
                        <td data-label="Telefonszám">${worker.telefonszam}</td>
                        <td data-label="Telephely">${worker.telephely}</td>
                        <td data-label="Jogosultság">${jogosultsag}</td>
                        <td data-label="Műveletek">
                            <button class="b-error c-white delete" data-id="${worker.id}">Törlés</button>
                            <a href="dolgozomodositas.php?id=${worker.id}"><button class="modify">Módosítás</button>
                        </td>
                    </tr>

                `;
            });

            $("#dolgozok").html(html);

        });
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