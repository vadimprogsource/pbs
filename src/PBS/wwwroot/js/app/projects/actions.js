jAction("projects")
.def
({
    all: function () { return this.GET("/project"); }
})
.view({ dataTemplate: "/views/projects/projects.html" })
.exec(function (x)
{
    $("#container").html(this.dataTemplate(x));
})
.useHistory();


jAction("project")
.def
({
    "new": function ()
    {
        if (!this.new$window)
        {
            this.new$window = new PopupWindowModal("#window-popup-modal").frame("#window-popup-content").dataTemplate(this.newProjectTemplate);
        }

        this.new$window.open({ projectCode: ''});
    },

    click : function(control)
    {
        for (var x = control; x; x = x.parentNode)
        {
            if (x.tagName === "TR")
            {
                this.edit(x.dataset.projectId);
                break;
            }
        }

    },

    createNew: function ()
    {
        this.new$window.close();
        return this.PUT("/project/create", this.new$window.dataBinding());
    },

    edit: function (id)
    {
        return this.GET("/project/" + id);
    },
    save: function ()
    {
        return this.PUT("/project",new DataBinder("#container form"));
    }
})
.view({ dataTemplate: "/views/projects/project.html", newProjectTemplate: "/views/projects/newProject.html" })
.exec(function (x)
{
    $("#container").html(this.dataTemplate(x));
})
.useHistory();