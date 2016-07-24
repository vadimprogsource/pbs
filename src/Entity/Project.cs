using Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity
{
    public class Project : Document, IProject
    {
        public string   Code { get; set; }
        public string Description { get; set; }
        public IJobActivityLog[] Logs { get; set; }
        public IPBSEntity Product { get; set; }
        public string Title { get; set; }


        public Project AssignFrom(IProject project)
        {

       
            Code        = project.Code;
            Title       = project.Title;
            Description = project.Description;

            return this;
        }
       
    }
}
