using Api;
using Api.Repository;
using Entity.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Security
{
    public class SecurityContext : ISecurityContext
    {
        public IAuthSession Session
        {
            get;
            set;
        }

        public bool IsAdminRole
        {
            get
            {
                return Session != null && Session.IsAuthenticated && repository.GetAdminFlag(Session.Sid);
            }
        }

        private readonly IUserRepository repository;

        public SecurityContext(IUserRepository repository)
        {
            Session = new Session { Expired = DateTime.MinValue, CreatedOn = DateTime.MinValue, User = null};
            this.repository = repository;
        }


    }
}
