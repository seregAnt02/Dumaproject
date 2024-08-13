using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
namespace duma.Models
{    
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
        }
        //Класс пользователей:
        public int Year { get; set; }
        /*public async Task<ClaimsIdentity> GenerateUserIdentityAsync
                                 (UserManager<ApplicationUser> manager)
         {
             var userIdentity = await manager.CreateIdentityAsync(this,
                                     DefaultAuthenticationTypes.ApplicationCookie);
             return userIdentity;
         }*/

    }
}