using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repository
{
    public interface IUserRepository
    {


        bool GetAdminFlag(Guid sid);

        Guid  CreateNewUser(string userName);
        void  SetUserPassword    (Guid uid , Guid password);

        bool TryFindUser(string loginName, Guid password,out Guid uid);
        bool TryFindUser(string loginName, out Guid uid);

        IUserAccount UpdateUser(Guid uid , IUserAccount user);

        void RemoveUser(Guid uid);

        IEnumerable<IUserAccount> GetUsers();

        IUserAccount GetUser(Guid uid);

        IAuthSession GetSession(Guid sid);
        IAuthSession CreateNewSession(Guid uid,TimeSpan expirationSessionTimeout);

        void RemoveSession(Guid sid);
        void RemoveExpiredSessions();


        IUserAccount GetUserBySession(Guid sid);


    }
}
