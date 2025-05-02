class api
{
    constructor(root)
    {
        this.root = root;
    }

    static setapidata(action, data)
    {
        return { ...data, action };
    }

    async get(action, data = [], callback)
    {
        $.get(this.root,api.setapidata(action,data))
            .done(function(response)
            {
                callback(response);
            });
    }

    async post(action, data = [], callback)
    {
        $.post(this.root,api.setapidata(action,data))
            .done(function(response)
            {
                callback(response);
            });
    }
    
    async put(action, data = [], callback)
    {
        $.ajax
        ({
            url: this.root,
            type: 'PUT',
            data: api.setapidata(action,data),
            success: function(response)
            {
                callback(response);
            }
        });
    }
    
    async delete(action, data = [], callback)
    {
        $.ajax
        (
        {
            url: this.root,
            type: 'DELETE',
            data: api.setapidata(action,data),
            success: function(response)
            {
                callback(response);
            }
        });
    }

}