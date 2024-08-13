using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using duma.Models;
using duma.Infrastructure;
using System.Collections.Generic;
using System.Web.Mvc;
namespace duma
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
        : base(store)
        {
        }
        //Класс менеджера пользователей наследуется от UserManager.
        //В конструкторе он принимает объект хранилища пользователей IUserStore. 
        //А статический метод Create() создает экземпляр класса ApplicationUserManager с помощью объекта контекста IOwinContext
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
        IOwinContext context)
        {
            DumContext db = context.Get<DumContext>();            
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));            
            //var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Настройка логики проверки имен пользователей
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false, //если равно true, то юзернейм должен содержать только алфавитно-цифровые символы
                RequireUniqueEmail = false //если равно true, то email пользователя должен быть уникальным
            };

            // Настройка логики проверки паролей
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 1, //минимальная длина пароля
                RequireNonLetterOrDigit = false, //если равно true, то пароль должен будет иметь как минимум один символ, который не является алфавитно-цифровым
                RequireDigit = false, //если равно true, то пароль должен будет иметь как минимум одну цифру
                RequireLowercase = false, //если равно true, то пароль должен будет иметь как минимум один символ в нижнем регистре
                RequireUppercase = false, //если равно true, то пароль должен будет иметь как минимум один символ в верхнем регистре
            };

            // Настройка параметров блокировки по умолчанию
            manager.UserLockoutEnabledByDefault = true; //Если true, то будет включена блокировка пользователей при создании пользователей
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5); //Количество времени по умолчанию, что пользователь заблокирован
            manager.MaxFailedAccessAttemptsBeforeLockout = 5; //Количество попыток доступа, разрешенных до блокировки пользователя (если блокировка включена// )                                                              

            // Регистрация поставщиков двухфакторной проверки подлинности. Для получения кода проверки пользователя в данном приложении используется телефон и сообщения электронной почты
            // Здесь можно указать собственный поставщик и подключить его.
            /*manager.RegisterTwoFactorProvider("Код, полученный по телефону", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Ваш код безопасности: {0}"
            });*/
            manager.RegisterTwoFactorProvider("Код из сообщения", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Код безопасности",
                BodyFormat = "Ваш код безопасности: {0}"
            });
            manager.EmailService = new EmailService();
            //manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }            
            return manager;
        }
    }
}