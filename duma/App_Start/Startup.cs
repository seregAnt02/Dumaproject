using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using duma.Infrastructure;
using duma.Models;

[assembly: OwinStartup(typeof(duma.Startup))]

namespace duma
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //Интерфейс IAppBuilder определяет множество методов, в данном случае нам достаточно трех методов.
            //Метод CreatePerOwinContext регистрирует в OWIN менеджер пользователей ApplicationUserManager и класс контекста DumContext.

            // настраиваем контекст и менеджер
            app.CreatePerOwinContext(DumContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // регистрация менеджера ролей
            //Благодаря регистрации менеджер ролей будет использовать тот же контекст данных, что и менеджер пользователей.
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            //Метод UseCookieAuthentication с помощью объекта CookieAuthenticationOptions устанавливает аутентификацию на основе куки 
            //в качестве типа аутентификации в приложении.А свойство LoginPath позволяет установить адрес URL,
            //по которому будут перенаправляться неавторизованные пользователи. Как правило, это адрес / Account / Login.
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }
    }
}