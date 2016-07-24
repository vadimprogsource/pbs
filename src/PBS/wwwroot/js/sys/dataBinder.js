function DataBinder(form)
{

    if (typeof form === 'string' || form instanceof String)
    {
        form = $(form).first()[0];
    }


    for (var i = 0,binding = form.querySelectorAll("[data-bind]") ; binding &&  i < binding.length; i++)
    {
        var element = binding[i];

        if (element)
        {

            if (element.type && element.type === "checkbox")
            {
                if(element.checked)
                {
                    this[element.dataset.bind] = element.value;
                }

                continue;
            }



            this[element.dataset.bind] = element.value;
        }
    }
}



