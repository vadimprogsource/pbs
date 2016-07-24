using Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity.Security
{
    public class User :  IUserAccount ,IPerson ,  IDeveloper
    {
        public Guid Id { get; set; }
       
        public bool IsAdmin { get; set; }

        public bool IsActive { get; set; }
       
        public string LoginName { get; set; }
       
        public string Title { get; set; }

        public Guid? Password { get; set; }

        public DateTime ModifiedOn { get; set; }

        public string Skype { get; set; }
       
        public string Phone { get; set; }
       
        public string Email { get; set; }

        public string Description { get; set; }

    }
}
