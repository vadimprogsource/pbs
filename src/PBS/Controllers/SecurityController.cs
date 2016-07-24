using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Api;
using PBS.Models;

namespace PBS.Controllers
{
    [Route("admin")]
    public class SecurityController : Controller
    {

        private readonly ISecurityService service;

        public SecurityController(ISecurityService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IEnumerable<IUserAccount> GetUsers()
        {
            return service.GetUsers().Select(x=>new UserAccountModel(x));
        }

        [HttpGet("{loginName}")]
        public IUserAccount GetUser(string loginName)
        {
            return service.GetUser(loginName);
        }

        [HttpPost]
        public IUserAccount CreateUser([FromBody]UserAccountModel model)
        {
            return service.CreateUserAccout( model.LoginName);
        }


        [HttpPost("all")]
        public IEnumerable<IUserAccount> CreateAndGetUsers([FromBody]UserAccountModel model)
        {
            CreateUser(model);
            return GetUsers();
        }

        [HttpPost("password")]
        public bool SetPassword([FromBody]ChangePasswordModel model)
        {

            if (string.IsNullOrWhiteSpace(model.LoginName))
            {
                return service.SetPassword(model.NewPassword, model.ConfirmPassword);
            }

            return service.SetPassword(model.LoginName , model.NewPassword, model.ConfirmPassword);
        }


   

        [HttpPut("all")]
        public IEnumerable<IUserAccount> UpdateAndGetUsers([FromBody]UserAccountModel model)
        {
            UpdateUser(model);
            return GetUsers();
        }

        [HttpPut]
        public void UpdateUser([FromBody]UserAccountModel model)
        {
            service.UpdateUserAccout( model);
        }


        [HttpDelete("{loginName}/all")]
        public IEnumerable<IUserAccount> DeleteAndGetUsers(string loginName)
        {
            service.RemoveUserAccount( loginName);
            return GetUsers();
        }



        [HttpDelete("{loginName}")]
        public void DeleteUser(string loginName)
        {
            service.RemoveUserAccount(loginName);
        }


    }
}
