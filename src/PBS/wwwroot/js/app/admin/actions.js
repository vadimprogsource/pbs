jAction("admin")
.def
({
    all: function () { return this.GET("/admin"); }
    ,
    "new":function()
    {

        if (!this.new$window)
        {
            this.new$window = new PopupWindowModal("#window-popup-modal").frame("#window-popup-content").dataTemplate(this.newUserTemplate);
        }

        this.new$window.open({ loginName: '', title: '' });

    },

    click : function(control)
    {

        for (var x = control; x; x = x.parentNode)
        {
            if (x.tagName === "TR")
            {
                this.edit(x.dataset.userLogin);
                break;
            }
        }

    },

    createNew : function ()
    {
        return this.POST("/admin/all", this.new$window.dataBinding());
    },

    edit: function (loginName)
    {
        return this.GET("/admin/" + loginName);
    },

    save: function ()
    {
        return this.PUT("/admin/all", this.edit$window.dataBinding());
    },
 
    "delete": function (loginName)
    {

        if (confirm("Remove user ?"))
        {
            return this.DELETE("/admin/"+loginName+"/all");
        }

        return false;
    }

})
.view({ dataTemplate: "/views/admin/users.html", newUserTemplate: '/views/admin/newUser.html', editUserTemplate: '/views/admin/editUser.html'})
.exec(function (x)
{

    if (Array.isArray(x))
    {
        $("#container").html(this.dataTemplate({ users: x }));

        if (this.edit$window)
        {
            this.edit$window.close();
        }

        if (this.new$window)
        {
            this.new$window.close();
        }

        return;
    }


    if (!this.edit$window)
    {
        this.edit$window = new PopupWindowModal("#window-popup-modal")
                            .frame       ("#window-popup-content")
                            .dataTemplate(this.editUserTemplate);
    }

    this.edit$window.open(x);


});


jAction("password")
.def
({
    setPassword: function (loginName)
    {


        if (!this.$window)
        {
            this.$window = new PopupWindowModal("#window-popup-modal")
                        .frame("#window-popup-content")
                        .dataTemplate(this.dataTemplate);
        }


        this.$window.open({ loginName: loginName, newPassword: '', confirmPassword: '' });
    },

    applySetPassword : function()
    {
        return this.POST("/admin/password", this.$window.dataBinding());
       
    }
})
.view({dataTemplate:'/views/admin/password.html'})
.exec(function(x)
{
    if(x)
    {
        alert("Password changed was successful !");
        this.$window.close();
        return;
    }

    alert("Password not change!");
    
});