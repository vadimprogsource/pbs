using Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PBS.Models
{
    public class UpdateProjectModel  : IProject , IEntity
    {

        public Guid Id      { get; set; }
        public string  Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        IPBSEntity IProject.Product
        {
            get
            {
                return null;
            }
        }

        IJobActivityLog[] IProject.Logs
        {
            get
            {
                return null;
            }
        }
    }
}
