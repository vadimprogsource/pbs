using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public  interface IUserAccount
    {
        DateTime ModifiedOn { get; }
        bool   IsAdmin   { get; }
        string Skype    { get; }
        string Phone    { get; }
        string Email    { get; }
        string LoginName{ get; }
        string Title    { get; }
        string Description { get; }

    }
}
