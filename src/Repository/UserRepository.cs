using Api;
using Api.Repository;
using Entity.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : DataRepository , IUserRepository
    {

        public UserRepository(IConfig cfg) : base(cfg)
        {

        }

        public IAuthSession CreateNewSession(Guid uid, TimeSpan expirationSessionTimeout)
        {
            Database.ExecuteSqlCommand("delete from sys_sessions where session_expired_dt <GetDate() or session_user_id='" + uid + "'");
            SaveChanges();

            Session session = new Session(Set<User>().AsNoTracking().FirstOrDefault(x => x.Id == uid) , expirationSessionTimeout);


            if (session.IsAuthenticated)
            {
                session.User = null;
                Set<Session>().Add(session);
                SaveChanges();

            }
          
            return session;
        }


        public IAuthSession GetSession(Guid sid)
        {
            return Set<Session>().AsNoTracking().FirstOrDefault(x => x.Sid == sid);
        }

        public IUserAccount GetUser(Guid uid)
        {
            return Set<User>().AsNoTracking().FirstOrDefault(x => x.Id == uid);
        }

        public IUserAccount GetUserBySession(Guid sid)
        {
            return Set<Session>().AsNoTracking().Where(x => x.Sid == sid).Select(x=>x.User).FirstOrDefault();
        }

        public IEnumerable<IUserAccount> GetUsers()
        {
            return Set<User>().AsNoTracking().Where(x=>x.IsActive).OrderBy(x=>x.LoginName).ToArray();
        }

        public void RemoveExpiredSessions()
        {
            Database.ExecuteSqlCommand("delete from sys_sessions where session_expired_dt <GetDate()");
            SaveChanges();
        }

        public void RemoveSession(Guid sid)
        {
            Database.ExecuteSqlCommand("delete from sys_sessions where session_expired_dt <GetDate() or session_id='"+sid+"'");
            SaveChanges();
        }

    

        public void SetUserPassword(Guid uid, Guid password)
        {
            DbSet<User> userSet = Set<User>();

            User userEntity = userSet.FirstOrDefault(x => x.Id == uid);

            if (userEntity != null)
            {
                userEntity.IsActive   = true;
                userEntity.Password   = password;
                userEntity.ModifiedOn = DateTime.Now;
                SaveChanges();
            }
        }

        public bool TryFindUser(string loginName, out Guid uid)
        {
            User user = Set<User>().AsNoTracking().FirstOrDefault(x => x.LoginName == loginName && x.IsActive);

            if (user != null)
            {
                uid = user.Id;
                return true;
            }

            return false;
        }

        public bool TryFindUser(string loginName, Guid password, out Guid uid)
        {
            User user = Set<User>().AsNoTracking().FirstOrDefault(x => x.LoginName == loginName && x.Password.HasValue && x.IsActive &&  x.Password == password);

            if (user != null)
            {
                uid = user.Id;
                return true;
            }

            return false;
        }


        public Guid CreateNewUser(string userName)
        {

            DbSet<User> userSet = Set<User>();

            User user = userSet.FirstOrDefault(x => x.LoginName == userName);


            if (user == null)
            {
                user = new User { Id = Guid.NewGuid(), IsActive = true, IsAdmin = false, LoginName = userName, Password = null, Title = userName , ModifiedOn = DateTime.Now};
                userSet.Add(user);
                SaveChanges();
            }


            if (!user.IsActive)
            {
                user.IsActive = true;
                SaveChanges();

            }

            return user.Id;
        }

        public IUserAccount UpdateUser(Guid uid, IUserAccount user)
        {

            DbSet<User> userSet = Set<User>();


            User userEntity = userSet.FirstOrDefault(x => x.Id == uid);

            if (userEntity != null)
            {
                userEntity.Title   = user.Title;
                userEntity.Skype   = user.Skype;
                userEntity.Phone   = user.Phone;
                userEntity.Email   = user.Email;

                userEntity.Description = user.Description;

                if (user.IsAdmin)
                {
                    userEntity.IsAdmin = true;
                     
                    foreach (User x in userSet.Where(x => x.Id != uid && x.IsAdmin))
                    {
                        x.IsAdmin = false;
                    }
                }

                userEntity.ModifiedOn = DateTime.Now;
                SaveChanges();

            }


            return userEntity;
        }


        public void RemoveUser(Guid uid)
        {
            DbSet<User> userSet = Set<User>();

            User userEntity = userSet.FirstOrDefault(x => x.Id == uid);

            if (userEntity != null && !userEntity.IsAdmin)
            {
                userEntity.IsActive = false;
                userEntity.ModifiedOn = DateTime.Now; 
                SaveChanges();
            }

        }

        public bool GetAdminFlag(Guid sid)
        {
            return Set<Session>().AsNoTracking().Any(x => x.Sid == sid && x.User.IsActive && x.User.IsAdmin);
        }
    }
}
