function HttpLoader(context ,compiler ,  obj)
{
    this.context  = context;
    this.compiler = compiler;
    this.tasks    = [];

    for (var x in obj)
    {
        this.tasks.push({ key: x, uri: new Uri(obj[x]).makeRelativeView().toString()});
    }

    this.execNextLoad();

}



HttpLoader.prototype.execNextLoad = function ()
{
    if (this.tasks && this.tasks.length)
    {
        var task = this.tasks.pop();

        if (task)
        {
            var httpClient = new HttpClient("GET", task.uri);
            var $loader    = this;
         
            httpClient.onReceive = function (html)
            {
                $loader.context[task.key] = $loader.compiler(html);
                $loader.execNextLoad();
            }

            httpClient.onError = function ()
            {
                $loader.execNextLoad();
            }

            httpClient.executeRequest("text/html");
        }

    }
}
