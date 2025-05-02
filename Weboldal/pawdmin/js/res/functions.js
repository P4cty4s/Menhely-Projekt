
    function locate(location)
    {
        window.location.href = location;
    }

    function isDataExist(data)
    {
        if (data == undefined || data == false)
        {
            locate("error.php");
        }
    }

    function getwindowwidth()
    {
        return window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth;
    }

    function GET(key)
    {
        const urlParams = new URLSearchParams(window.location.search);
        const result = urlParams.get(key);
        return result;
    }

    function hideelement($element)
    {
        $element.removeClass('visible').addClass('hidden');
    }

    function showelement($element)
    {
        $element.removeClass('hidden').addClass('visible');
    }

    function toggleelement($element)
    {
        if ($element.hasClass('visible'))
        {
            hideelement($element);
        }
        else
        {
            showelement($element);
        }
    }

    function setError($element)
    {
        $element.removeClass('success').addClass('error');
    }

    function setSuccess($element)
    {
        $element.removeClass('error').addClass('success');
    }

    function clearStatus($element)
    {
        $element.removeClass('error success');
    }