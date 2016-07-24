function PopupWindowModal(windowPopup)
{
    this.$window = $(windowPopup);
}


PopupWindowModal.prototype.frame = function (contentControl)
{
    this.$content = $(contentControl);
    return this;
}


PopupWindowModal.prototype.dataTemplate = function (lambda)
{
    this.$templateCompiler = lambda;
    return this;
}

PopupWindowModal.prototype.open = function (obj)
{

    if (this.$templateCompiler)
    {
        obj = this.$templateCompiler(obj);
    }


    if (this.$content)
    {
        this.$content.html(obj);
    }
    else
    {
       this.$window.html(obj);
    }

    this.$window.modal();
    return this;
}

PopupWindowModal.prototype.close = function ()
{
    this.$window.modal("hide");
}

PopupWindowModal.prototype.dataBinding = function (groupControl)
{
    if (!groupControl)
    {
        groupControl = 'form';
    }

    return new DataBinder(this.$window.find(groupControl)[0]);
}

