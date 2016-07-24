using Api;
using Api.Repository;
using Sys.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Security
{
    public class AuthProvider : IAuthProvider, ISecurityService
    {

        private readonly IUserRepository   repository;
        private readonly IConfig           cfg;
        private readonly ISecurityContext context;


        private Guid GetPasswordHash(string password)
        {
            return new HashCodeBuilder().Append(password).ToGuid();
        }


        public AuthProvider(IUserRepository repository , IConfig cfg,ISecurityContext context)
        {
            this.repository = repository;
            this.cfg = cfg;
            this.context = context;
        }

        public IAuthSession Auth(Guid sid)
        {
            return repository.GetSession(sid);
        }

        public IAuthSession LogIn(string userName, string password)
        {
            Guid uid;

            if (repository.TryFindUser(userName, GetPasswordHash(password), out uid))
            {
                return repository.CreateNewSession(uid, cfg.SessionExpirationTimeout);
            }

            return null;

        }

        public void LogOut(Guid sid)
        {
            repository.RemoveSession(sid);
            repository.RemoveExpiredSessions();
        }



        private bool IsAdminPermission
        {

            get
            {
                if (context.Session.IsAuthenticated)
                {
                    return repository.GetUserBySession(context.Session.Sid).IsAdmin;
                }

                return false;

            }



        }



        public IUserAccount CreateUserAccout( string loginName)
        {

            if (context.IsAdminRole &&  !string.IsNullOrWhiteSpace(loginName))
            {
                Guid uid = repository.CreateNewUser(loginName);
                return repository.GetUser(uid);
            }

            return null;
        }

        public void UpdateUserAccout(IUserAccount account)
        {
            if (context.IsAdminRole)
            {

                Guid uid;

                if (repository.TryFindUser(account.LoginName, out uid))
                {
                    repository.UpdateUser(uid, account);
                }
            }
        }

        public void RemoveUserAccount( string loginName)
        {
            if (context.IsAdminRole)
            {

                Guid uid;

                if (repository.TryFindUser(loginName, out uid))
                {
                    repository.RemoveUser(uid);
                }
            }
        }

        public IEnumerable<IUserAccount> GetUsers()
        {
            if (context.IsAdminRole)
            {
                return repository.GetUsers();
            }

            return Enumerable.Empty<IUserAccount>();
        }


        public IUserAccount GetUser(string loginName)
        {

            Guid uid;

            if (context.IsAdminRole && repository.TryFindUser(loginName,out uid))
            {
                return repository.GetUser(uid);
            }

            return null;
        }


        private bool ApplySetPassword(Guid uid,string loginName, string newPassword, string confirmPassword)
        {
            Guid newGuid = GetPasswordHash(newPassword), cGuid = GetPasswordHash(confirmPassword);



            if (newGuid == cGuid)
            {
                if (repository.TryFindUser(loginName, newGuid, out cGuid) && cGuid != uid)
                {
                    return false;
                }


            }


            if (newGuid == cGuid && !repository.TryFindUser(loginName, newGuid, out cGuid))
            {
                repository.SetUserPassword(uid, newGuid);
                return true;
            }

            return false;
        }

        public bool SetPassword(string loginName,  string newPassword, string confirmPassword)
        {

            if (context.IsAdminRole)
            {
                Guid uid;

                if (repository.TryFindUser(loginName, out uid))
                {
                    return ApplySetPassword(uid,loginName ,  newPassword, confirmPassword);
                }
             
            }

            return false;

        }

        public bool SetPassword(string newPassword, string confirmPassword)
        {
            if (context.Session.IsAuthenticated)
            {
                IUserAccount user = repository.GetUserBySession(context.Session.Sid);

                if (user != null)
                {
                    return ApplySetPassword(context.Session.Uid, user.LoginName, newPassword, confirmPassword);
                }
            }

            return false;
        }

        public bool IsMembersAccessOf(string rootPath)
        {

            if (string.IsNullOrWhiteSpace(rootPath))
            {
                return false;
            }


            return cfg.AuthAccessOnly.Contains(rootPath.ToLowerInvariant());
        }

      
    }
}
