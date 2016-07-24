Uri.hostApi   = "";
Uri.hostView = "";


function Uri(url)
{
    this.absoluteUri = url;

}



Uri.prototype.makeRelativeApi = function ()
{
    if(Uri.hostApi)
    {
        this.absoluteUri = Uri.hostApi + this.absoluteUri;
    }

    return this;
}


Uri.prototype.makeRelativeView= function ()
{
    if (Uri.hostView)
    {
        this.absoluteUri = Uri.hostView + this.absoluteUri;
    }

    return this;
}


Uri.prototype.toString = function ()
{
    return this.absoluteUri;
}