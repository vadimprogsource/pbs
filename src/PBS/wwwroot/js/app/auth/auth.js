jAction("auth")
.def
({
    signIn: function (user,password)
    {
        return this.POST("/auth/signin", { User: user, Password: password });
    }
    ,
    signOut: function ()
    {
        return this.POST("/auth/signout", this.session);
    },
    onLogin: function (lambda)
    {
        this.$_enter_event = lambda;
        return this;
    },
    onLogOut: function (lambda)
    {
        this.$_exit_event = lambda;
        return this;
    },
    onLoginFailed: function(lambda)
    {
        this.$_failed_event = lambda;
        return this;
    },


    useSession : function (x)
    {

        jAction.clearHistory();

        if (x.isValid)
        {
            HttpClient.userSession = x.sid;
            this.session = x;
            return true;
        }

        HttpClient.userSession = null;
        this.session = {};
        return false;

    }

})
.exec(function (x)
{
    if (this.useSession(x))
    {
        if (this.$_enter_event)
        {
            this.$_enter_event.call(this);
        }

        return;
    }


    if (x.sid && this.$_failed_event)
    {
        this.$_failed_event.call(this);
    }


    this.$_exit_event.call(this);
});