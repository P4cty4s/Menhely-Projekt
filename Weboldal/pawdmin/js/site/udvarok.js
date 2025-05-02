class siteController
{
    constructor()
    {
        this.dataapi = new api('../php/api/data.api.php');
        this.kutyaapi = new api('../php/api/kutyak.api.php');
        $(".trash").toggle();

        // draw
        this.getTelehpelyek();
        this.getKennelNelkuliKutyak();
        this.getKennelek();

        // buttons
        this.switchTelephely();
        this.addKennel();
        this.deleteKennel();
        this.DragDrop();
        this.searchKutyak();
        this.modifyUdvar();
        this.changeUdvarNev();
        this.deleteUdvar();
        this.addUdvar();
        this.close_openKennel();
        this.mobileDrop();
    }

    mobileDrop()
    {
        const _this = this;
        var selectedDog = false;
        var kutyaid;
        var regiKennelid;
        
        $(document).on('click','.drag',function(e)
        {
            e.stopPropagation();
            $(e.target).addClass('b-error');

            console.log('klikk kutya');
            selectedDog = true;
            $(".kennel").css('background-color', 'green');
            $(".trash").toggle();
            $(".trash").css('background-color','red');
            kutyaid = $(e.target).data('id');
            console.log($(e.target));
            if ($(e.target).parent().hasClass('kennel'))
            {
                regiKennelid = $(e.target).parent().data('id');
            }
            else
            {
                console.log('HUB');
                regiKennelid = false;
            }
        });

        $(document).on('click','.kennel',function(e)
        {
            if (selectedDog)
            {
                const dropZone = $(e.target);
                let data =
                {
                    kennel : dropZone.data('id'),
                    kutyaid : kutyaid,
                    regiKennelid : regiKennelid
                }
                _this.kutyaapi.put('change-kennel',data,function(response)
                {
                    const udvarid = $("#telephely").val();
                    _this.getKennelNelkuliKutyak();
                    _this.getKennelek(udvarid);
                    $(".trash").toggle();
                });
            }
        });

    }

    close_openKennel()
    {
        $(document).on('click', '.close-open', function(e) {
            const $button = $(this);
            const $tr = $button.closest('tr');
            const $tds = $tr.find('td');
        
            // Azt a <td>-t keressük, ami nem tartalmazza a gombot
            const $targetTD = $tds.filter(function() {
                return !$(this).has($button).length;
            });
        
            if ($button.is('[open]')) {
                $button.removeAttr('open').attr('close', '').text('Kinyit');
                showelement($targetTD);
            } else {
                $button.removeAttr('close').attr('open', '').text('Lecsuk');
                hideelement($targetTD);
            }
        });
        
    }

    addUdvar()
    {
        const _this = this;
        $(document).on('click','.uj-udvar',function(e)
        {
            const telephelyid = $(e.target).data('telephelyid');
            _this.dataapi.post('upload-udvar',{telephelyid:telephelyid},function(response)
            {
                _this.getKennelek(telephelyid);
            });
        });
    }

    deleteUdvar()
    {
        const _this = this;
        $(document).on('click','.delete-udvar',function(e)
        {
            const udvarid = $(e.target).data('udvarid');
            const telephelyid = $("#telephely").val();
            
            _this.dataapi.delete('delete-udvar',{udvarid:udvarid},function(response)
            {
                const kennelek = response.data.kennelek;
                if (kennelek != false)
                {
                    _this.kutyaapi.delete('delete-kennel',{kennelek:kennelek},function(response)
                    {
                        _this.getKennelNelkuliKutyak();
                        _this.getKennelek(telephelyid);
                    });
                }
                else
                {
                    _this.getKennelNelkuliKutyak();
                    _this.getKennelek(telephelyid);
                }
            });

        });
    }

    changeUdvarNev()
    {
        const _this = this;
        $(document).on('click','.new-udvarnev',function(e)
        {
            const udvarid = $(e.target).data('udvarid');
            let data =
            {
                udvarid : udvarid,
                nev : $(`.udvarnev[data-udvarid="${udvarid}"]`).val()
            }
            _this.dataapi.put('change-udvarnev',data,function(response)
            {
                const telephelyid = $("#telephely").val();
                _this.getKennelek(telephelyid);
            });
        });
    }

    modifyUdvar()
    {
        let blurException = false;
        $(document).on('mousedown', '.new-udvarnev, .delete-udvar', function()
        {
            blurException = true;
        });

        $(document).on('focus','.udvarnev',function(e)
        {

            var udvarid = $(e.target).data('udvarid');
            var confirmButton = $(`.new-udvarnev[data-udvarid="${udvarid}"]`);
            var deleteButton = $(`.delete-udvar[data-udvarid="${udvarid}"]`);
            showelement(confirmButton);
            showelement(deleteButton);
        });

        $(document).on('blur','.udvarnev',function(e)
        {
            if (blurException)
            {
                blurException = false;
                return;
            }

            var udvarid = $(e.target).data('udvarid');
            var confirmButton = $(`.new-udvarnev[data-udvarid="${udvarid}"]`);
            var deleteButton = $(`.delete-udvar[data-udvarid="${udvarid}"]`);
            hideelement(confirmButton);
            hideelement(deleteButton);
        });
    }

    searchKutyak()
    {
        $(document).on('keyup','#nev',function(e)
        {
            const _this = this;
            let nev = $(e.target).val();
            console.log(nev);
            let data =
            {
                hasoffset : false,
                needimg : false,
                nev : [nev]
            };
            console.log(data);

            this.kutyaapi.get('get-dogs',data,function(response)
            {
                const kutyak = response.data.kutyak;
                _this.getKennelNelkuliKutyak(kutyak);
            });
        }.bind(this));
    }

    DragDrop()
    {
        const _this = this;
        var regiKennelid;
        // drag
        $(document).on('dragstart','.drag',function(e)
        {
            const id = $(e.target).data('id');
            if ($(e.target).parent().hasClass('kennel'))
            {
                regiKennelid = $(e.target).parent().data('id');
                $('.trash').toggle();
                $('.trash').css('background-color','red');
            }
            else
            {
                regiKennelid = false;
            }

            e.originalEvent.dataTransfer.setData('text/plain',id);
            console.log('drag => ',id);
        });

        // hover to drop
        $(document).on('dragover','.drop',function(e)
        {
            e.preventDefault();
            $(".kennel").css('background-color', 'green');
            $('.trash').css('background-color','red');
        });

        // drop
        $(document).on('drop','.drop',function(e)
        {
            $('.trash').hide();
            const id = e.originalEvent.dataTransfer.getData('text/plain');
            const dropzone = $(this);
            console.log('kennel => ',regiKennelid);
            const data =
            {
                hasoffset: false,
                needimg: false,
                id : id,
            };
            _this.kutyaapi.get('get-dogs',data,function(response)
            {
                const kutya = response.data.kutyak[0];
                const kutyabox = $(`<a href="#" class="drag" data-id="${kutya.id}">${kutya.nev}</a>`);
                !$(e.target).hasClass('trash') && dropzone.append(kutyabox);
            });

            const putdata =
            {
                kennel : dropzone.data('id'),
                kutyaid : id,
                regiKennelid: regiKennelid
            };
            _this.kutyaapi.put('change-kennel',putdata,function(resposne)
            {
                const udvarid = $("#telephely").val();
                _this.getKennelNelkuliKutyak();
                _this.getKennelek(udvarid);
            });

        });

    }

    deleteKennel()
    {
        $(document).on('click','.delete',function(e)
        {
            const datas = $(e.target).parent();

            let kennelek = [];
            const _this = this;
            const kennelid = datas.data('kennelid');
            const udvarid = datas.data('udvarid');
            const telephelyid = datas.data('telephelyid');

            kennelek.push(kennelid);

            const data = { kennelek : kennelek, udvarid : udvarid };
            this.kutyaapi.delete('delete-kennel',data,function()
            {
                _this.getKennelNelkuliKutyak();
                _this.getKennelek(telephelyid);
            });
        }.bind(this));
    }

    addKennel()
    {
        $(document).on('click','.add-kennel',function(e)
        {
            const _this = this;
            const udvarid = $(e.target).data('udvarid');
            const telephelyid = $(e.target).data('telephelyid');
            this.kutyaapi.post('new-kennel',{udvarid:udvarid},function()
            {
                _this.getKennelek(telephelyid);
            });
        }.bind(this));
    }

    switchTelephely()
    {
        $(document).on('change','#telephely',function(e)
        {
            const telephelyid = $(e.target).val();
            this.getKennelek(telephelyid);
        }.bind(this));
    }

    getKennelNelkuliKutyak(kutyak=false)
    {
        let html = ``;
        if (!kutyak)
        {
            const data =
            {
                hasoffset : false,
                needimg : false,
                kennel : 0
            };
            this.kutyaapi.get('get-dogs',data,function(response)
            {
                const kutyak = response.data.kutyak;
                kutyak.forEach((kutya) =>
                {
                    html += `<div class="tag drag" draggable="true" data-id="${kutya.id}">${kutya.nev}</div>`;
                });
                $(".kennel-nelkuli-kutyak").html(html);
            });
        }
        else
        {
            kutyak.forEach((kutya) =>
            {
                html += `<div class="tag drag" draggable="true" data-id="${kutya.id}">${kutya.nev}</div>`;
            });

            $(".kennel-nelkuli-kutyak").html(html);
        }
    }

    getTelehpelyek()
    {
        const _this = this;
        let html = ``;
        this.dataapi.get('get-telephely',{},function(response)
        {   
            const telephelyek = response.data.telephelyek;
            telephelyek.forEach((telephely) =>
            {
                html += `<option value="${telephely.id}">${telephely.nev}</option>`;
            });
            $("#telephely").html(html);
            console.log('telehelyek lekérve')
        });
    }

    getKennelek(telephelyid=false)
    {
        
        const data =
        {
            telephelyid : telephelyid
        };
        this.kutyaapi.get('get-kennelek',data,function(response)
        {
            const udvarok = response.data.udvarok;
            const telephely = response.data.telephely;

            // fejléc
            var html = `<h3>${telephely.nev} udvarai</h3>`;
            html += `<a href="#" class="uj-udvar" data-telephelyid="${telephely.id}">Új udvar hozzáadása</a>`;

            // udvarok
            udvarok.forEach((udvar) =>
            {
                var kennelhtml = ``;
                let kennelek = udvar.kennelek;

                // kennelek
                kennelek.forEach((kennel) =>
                {
                    var kutyakhtml = ``;
                    let kutyak = kennel.kutyak;

                    // kutyák
                    if (kutyak[0] != false)
                    {
                        kutyak.forEach((kutya) =>
                        {
                            kutyakhtml += `<a href="#" class="tag drag" data-id="${kutya.id}">${kutya.nev}</a>`;
                        });
                    }
                    
                    kennelhtml += `
                        <tr class="kennel-tr">
                            <td data-label="kennelszam" class="kennelszam">
                                ${kennel.kennelszam}
                                <c-button class="delete c-dark-brown" type="delete" data-telephelyid="${telephelyid}" data-kennelid="${kennel.id}" data-udvarid="${udvar.id}">törlés</c-button>
                            </td>
                            <td data-label="kutyák" class="drop kennel" data-id="${kennel.id}">${kutyakhtml}</td>
                        </tr>
                    `;

                });

                html += `

                    <div class="col-12 col-md-6 col-lg-6 col-xl-4">
                    <input type="text" placeholder="${udvar.udvarnev}" value="${udvar.udvarnev}" class="udvarnev" data-udvarid="${udvar.id}">
                    <div class="modify">
                        <button class="new-udvarnev hidden" data-udvarid="${udvar.id}">Módosítás</button>
                        <button class="delete-udvar hidden" data-udvarid="${udvar.id}">Törlés</button>
                    </div>
                    <table class="">
                        <thead>
                            <tr>
                                <th scope="col">Kennelszám</th>
                                <th scope="col">Kutyák</th>
                            </tr>
                        </thead>
                        <tr class="table-controller">
                            <td colspan="2" class="add">
                                <button class="add-kennel" data-udvarid="${udvar.id}" data-telephelyid="${telephelyid}">Hozzáadás</button>
                            </td>
                        </tr>
                        ${kennelhtml}
                    </table>
                    </div>
                `;

            });
            
            $("#kennelek").html(html);

        });
    }
}

new siteController();
