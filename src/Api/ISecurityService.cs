using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public interface ISecurityService
    {
        IUserAccount CreateUserAccout (string loginName);
        void         UpdateUserAccout (IUserAccount account);
        void         RemoveUserAccount(string loginName);

        IEnumerable<IUserAccount> GetUsers();
        IUserAccount GetUser(string loginName);


        bool SetPassword(string loginName ,  string newPassword, string confirmPassword);
        bool SetPassword(string newPassword, string confirmPassword);

    }
}
