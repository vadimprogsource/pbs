using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public interface IAuthProvider
    {
        IAuthSession LogIn (string userName, string password);
        IAuthSession Auth  (Guid sid);
        void         LogOut(Guid sid);


        bool IsMembersAccessOf(string rootPath);
   

    }
}
