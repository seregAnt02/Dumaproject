using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using duma.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
namespace duma.Models
{
    class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        //Для управления ролями используется менеджер ролей RoleManager.
        //Опять же наследуем функционал от уже имеющегося класса RoleManager.
        //Метод Create позволит классу приложения OWIN создавать экземпляры менеджера ролей для обработки каждого запроса,
        //где идет обращение к хранилищу ролей RoleStore.
        public ApplicationRoleManager(RoleStore<ApplicationRole> store)
        : base(store)
        { }
        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options,
        IOwinContext context)
        {
            return new ApplicationRoleManager(new
            RoleStore<ApplicationRole>(context.Get<DumContext>()));
        }
    }
}