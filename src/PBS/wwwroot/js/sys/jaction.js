function jAction (actionName)
{
    var action = jAction[actionName];

    if (!action)
    {
        jAction[actionName] = action = new jAction.$();
    }

    return action;
}



jAction.registerTemplateCompiler = function (lambda)
{
    jAction.$.templateCompiler= lambda;
}


jAction.$ = function ()
{

}





jAction.$.history =
{
    head: null,
    stack: [],

    logToHistory: function (method, uri, body, actionContext)
    {
        if (this.head)
        {
            this.stack.push(this.head);
        }

        this.head = { method: method, uri: uri, body: body, actionContext: actionContext };

    },

    getBack: function ()
    {
        if (this.stack.length)
        {
            this.head = this.stack.pop();
            return this.head;
        }
    },

    clear: function ()
    {
        this.head = null;
        this.stack = [];
    }
};



jAction.clearHistory = function ()
{
    jAction.$.history.clear();
}

jAction.backToHistory = function ()
{
    var x = jAction.$.history.getBack();

        if (x)
        {
            var httpClient = new HttpClient(x.method, new Uri(x.uri).makeRelativeApi().toString());

            if (x.actionContext)
            {
                var actionContext = x.actionContext;

                httpClient.onReceive = function (x)
                {
                    actionContext.executeAction.call(actionContext, JSON.parse(x));
                }
            }

            httpClient.executeRequest("application/json", x.body);
        }
  
    return false;
};







jAction.$.prototype.def = function (obj)
{
     for (var x in obj)
     {
        this[x] = obj[x];
     }
     return this;
}

jAction.$.prototype.view = function (obj)
{

    var compiler = jAction.$.templateCompiler;

    if (!compiler && Handlebars)
    {
        compiler = Handlebars.compile;
    }

    new HttpLoader(this, compiler, obj);
    return this;
}


jAction.$.prototype.exec = function (lambda)
{
    this.executeAction = lambda;
    return this;
}


jAction.$.prototype.useHistory = function ()
{
    this.hasHistory = true;
    return this;
}


jAction.$.prototype.executeCore = function (method, uri, body)
{
    var httpClient = new HttpClient(method, new Uri(uri).makeRelativeApi().toString());


    if (body)
    {
        body = JSON.stringify(body);
    }



    if (this.executeAction)
    {
        var actionContext = this;

        httpClient.onReceive = function (x)
        {
          var obj = JSON.parse(x);
          actionContext.executeAction.call(actionContext, obj);
        }

        if (this.hasHistory)
       {
            jAction.$.history.logToHistory(method, uri, body, actionContext)
       }

    }

    httpClient.executeRequest("application/json",body);
    return false;
}


jAction.$.prototype.GET = function (url)
{
    return this.executeCore("GET", url);
}

jAction.$.prototype.POST = function (url, body)
{
    return this.executeCore("POST", url,body);
}

jAction.$.prototype.PUT = function (url, body)
{
 return this.executeCore("PUT", url, body);
}

jAction.$.prototype.DELETE = function (url)
{
    return this.executeCore("DELETE",url);
}

