class siteController
{
    constructor()
    {   
        this.api = new api('../php/api/data.api.php');


        this.drawTable();
        this.deleteBtn();
    }

    deleteBtn()
    {
        $(document).on('click','.deletebtn',function(e)
        {
            const id = $(e.target).data('id');
            const _this = this;
            this.api.delete('delete-telephely',{id:id},function(response)
            {
                if (response.status == false)
                {
                    popup.set("Nem törölhető, mert "+response.data.msg,'error');
                }
                else
                {
                    _this.drawTable();
                }
            });

        }.bind(this));
    }

    drawTable()
    {
        this.api.get('get-telephely',{},function(response)
        {
            const telephelyek = response.data.telephelyek;
            let html = ``;

            html += `

                <thead>
                    <tr>
                        <th scope="col">Megnevezés</th>
                        <th scope="col">Cím</th>
                        <th scope="col">Email</th>
                        <th scope="col">Telefonszam</th>
                        <th scope="col">Műveletek</th>
                    </tr>
                </thead>

            `;

            telephelyek.forEach((telephely) =>
            {
                html += `

                    <tr>
                        <td data-label="Megnevezés">${telephely.nev}</td>
                        <td data-label="Cím">${telephely.hely}</td>
                        <td data-label="Email">${telephely.email}</td>
                        <td data-label="Telefonszám">${telephely.telefonszam}</td>
                        <td>
                            <button class="deletebtn b-error c-white" data-id="${telephely.id}">Törlés</button>
                            <a href="telephelymodositas.php?id=${telephely.id}"><button>Módosítás</button></a>
                        </td>
                    </tr>

                `;
            });

            $("#telephelyek").html(html);
        });
    }

}

new siteController();