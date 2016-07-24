using Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity.Security
{
    public class Session : IAuthSession
    {
        public Guid Sid { get; set; }

        public Guid Uid { get; set; }

        public DateTime Expired { get; set; }

        public DateTime CreatedOn { get; set; }
       
        public bool IsAuthenticated
        {
            get
            {
                return Expired > DateTime.Now;
            }
        }

        public User User { get; set; }

        public Session() { }

        public Session(User user,TimeSpan timeout)
        {

            Sid       = Guid.NewGuid();
            CreatedOn = DateTime.Now;


            if (user == null)
            {
                Uid = Guid.Empty;
                Expired = DateTime.Now.AddHours(-1);
            }
            else
            {
                Uid = user.Id;
                Expired = DateTime.Now.Add(timeout);
                User = user;
            }

        }



    }
}
