class formController
{
    constructor()
    {
        this.setView();
        this.setController();
    }

    setView()
    {
        this.addClasses();
        this.setContainer();
        this.setLabel();
        this.setIcon();
        this.setPasswordIcon();
        this.removePFromFileInput();
    }

    setController()
    {
        this.disableFormReload();
        this.showPassword();
        formController.handleTriggerButton();
        this.checkInputs();
    }

    /* static */
    static getinputdata($input,key,asdom=false) { return asdom ? $($input.data(key)) : $input.data(key); }
    static getContainer($input) { return $input.closest(".input-container"); }
    static seterror($input) { $input.removeClass('success').addClass('error'); }
    static setsuccess($input) { $input.removeClass('error').addClass('success'); }
    static clearstatus($input) { $input.removeClass('error success'); }
    static emptyinput($input) { return $input.val().trim() === ''; }
    static disablebutton($button) { $button.attr('disabled',true); }
    static enablebutton($button) { $button.attr('disabled',false); }

    static setRule($input,interval=false,onlynum=false,onlyletter=false,$sameas=false)
    {
        let inputdata = $input.val().trim();
        let success = true;

        //interval
        if (interval != false)
        {
            if (interval.min != false)
            {
                if (inputdata.length < interval.min) success = false;
            }
            if (interval.max != false)
            {
                if (inputdata.length > interval.max) success = false;
            }
        }

        //onlynum
        if (onlynum && success)
        {
            if (!regex.onlynum(inputdata)) success = false; 
        }

        //onlyletter
        if (onlyletter && success)
        {
            if (!regex.onlyletter(inputdata)) success = false;
        }

        //same as
        if ($sameas != false && success)
        {
            if (inputdata != $sameas.val().trim()) success = false;
        }

        success ? formController.setsuccess($input) : formController.seterror($input);

    }
    static getformdata(formid)
    {
        const inputs = $(`#${formid} input, #${formid} select`);
        let result = {};

        inputs.each(function()
        {
            if ($(this).attr('type') != 'file')
            {
                let key = $(this).attr('id');
                let value = $(this).val()?.trim() ?? '';
                if ($(this).attr('type') === 'checkbox')
                {
                    value = $(this).is(':checked') ? 1 : 0; 
                }
                result[key] = value;
            }
        });
        return result;
    }


    /* controller */
    disableFormReload()
    {
        $("form").on("submit", function(e)
        {
            e.preventDefault();
        });
    }

    showPassword()
    {
        $(document).on('click','.showpass',function()
        {
            console.log('click');
            let input = $(this).siblings('input');
            let icon = $(this);
            if (input.attr('type') == 'password')
            {
                input.attr('type','text');
                icon.removeClass("fa-lock");
                icon.addClass("fa-lock-open")
            }
            else
            {
                input.attr('type','password');
                icon.removeClass("fa-lock-open");
                icon.addClass("fa-lock");
            }
        });
    }

    static handleTriggerButton(forceForm = null)
    {
        console.log('form check lefutott');

        const checkForm = ($form) =>
        {
            let $button = $form.find("[trigger]"); 
            let hasError = $form.find(".error").length > 0;
            let hasEmptyRequired = $form.find("input[req]").filter(function()
            {
                return formController.emptyinput($(this));
            }).length > 0; 
                
            if (hasError || hasEmptyRequired)
            {
                formController.disablebutton($button);
            }
            else
            {
                formController.enablebutton($button);
            }
        };

        if (forceForm)
        {
            checkForm(forceForm);
            return;
        }

        $("form").each(function()
        {
            checkForm($(this));
        });

        $(document).on("keyup", "input[req]", function()
        {
            setTimeout(() =>
            {
                let $form = $(this).closest("form");
                checkForm($form);
            },10);
        });
    }


    checkInputs()
    {
        const check = ($input) =>
        {
            if (formController.emptyinput($input)) formController.seterror($input);
            else formController.clearstatus($input);
        }

        $(document).on('keyup change','input[req]',function(){ check($(this)) });
    }

    /* view */
    removePFromFileInput()
    {
        document.addEventListener("DOMContentLoaded", function()
        {
            document.querySelectorAll("p:has(input[type='file'])").forEach(p =>
            {
                let fileInput = p.querySelector("input[type='file']");
                let fileLabel = p.querySelector("label");
                
                if (fileLabel)
                {
                    let parent = p.parentNode;
                    parent.insertBefore(fileLabel, p); 
                }

                if (fileInput)
                {
                    let parent = p.parentNode;
                    parent.insertBefore(fileInput, p); 
                }
                p.remove();
            });
        }); 
    }

    addClasses()
    {
        $("form").each(function()
        {
            $(this).addClass("box");
        });
    }

    setPasswordIcon()
    {
        $("input[type='password']").each(function()
        {
            let container = formController.getContainer($(this));
            let icon = $('<i class="fas fa-lock showpass"></i>');
            container.prepend(icon);
        });
    }

    setIcon()
    {
        $("input[data-icon]").each(function()
        {
            let container = formController.getContainer($(this));
            let icon = formController.getinputdata($(this),'icon',true);
            container.prepend(icon);
        });
    }

    setLabel()
    {
        $("input[data-label], select[data-label]").each(function()
        {
            let text = $(this).data('label');
            if ($(this).is('[req]')) text += ' *'; 
            let label = $(`<label>${text}</label>`);

            if ($(this).attr('type') != "checkbox")
            {
                $(this).before(label);
            }
            else
            {
                $(this).after(label);
            }
        });
    }

    setContainer()
    {
        $("input, select").each(function()
        {
            let container;
            switch($(this).attr('type'))
            {
                case 'checkbox':
                    container = $("<div class='input-container-checkbox'></div>");
                    $(this).wrap(container);
                    break;
                case 'file':
                    container = $("<div class='input-container drop-zone'></div>");
                    let p = $("<p>Húzd ide a fájlt vagy kattints a feltöltéshez!</p>");
                    container.append(p);
                    $(this).wrap(container);
                    break;
                default:
                    container = $("<div class='input-container'></div>");
                    $(this).wrap(container);
                    break;
            }
        });
    }

}

new formController();