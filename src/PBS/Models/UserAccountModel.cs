using Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PBS.Models
{
    public class UserAccountModel : IUserAccount
    {
        public string LoginName { get; set; }
        public string Title { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string Skype { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }


        public string Description { get; set; }


        public UserAccountModel() { }

        public UserAccountModel(IUserAccount account)
        {
            IsAdmin    = account.IsAdmin;
            LoginName  = account.LoginName;
            Skype      = account.Skype;
            Phone      = account.Phone;
            Email      = account.Email;    
            Title      = account.Title;
            ModifiedOn = account.ModifiedOn;

            Description = account.Description;
        }


    }
}
