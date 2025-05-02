class siteController
{
    constructor()
    {
        this.api = new api('../php/api/data.api.php');
        this.getWorkers();
        this.drawTable();
        this.handleSelector();
    }

    handleSelector()
    {
        const _this = this;
        $(document).on('change','select[selector]',function(e)
        {
            let data =
            {
                userid : $("#userid").val(),
                category : $("#category").val()
            };
            _this.drawTable(data);
        });
    }

    getWorkers()
    {
        this.api.get('get-workers',{},function(response)
        {
            const workers = response.data.workers;
            let html = ``;

            workers.forEach((worker) =>
            {
                html += `<option value="${worker.id}">${worker.username}</option>`;
            });

            $("#userid").html(html);
        });
    }

    drawTable(selector={})
    {
        this.api.get('get-changelog',selector,function(response)
        {
            const changelogs = response.data.changelogs == undefined ? [] : response.data.changelogs;
            let html = ``;

            html += `

                <thead>
                    <tr>
                        <td scope="col">Kategória</td>
                        <td scope="col">Dolgozó</td>
                        <td scope="col">Üzenet</td>
                        <td scope="col">Dátum</td>
                    </tr>
                </thead>

            `;

            changelogs.forEach((changelog) =>
            {
                let user = changelog.user;
                let cl = changelog.changelog;

                html += `

                <tr>
                    <td data-label="Kategória">${cl.category}</td>
                    <td data-label="Dolgozó">${user.username}</td>
                    <td data-label="Üzenet">${cl.msg}</td>
                    <td data-label="Dátum">${cl.date}</td>
                </tr>

                `;
            }); 

            $("#changelog").html(html);
        });
    }

}

new siteController();