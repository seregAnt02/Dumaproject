using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using duma.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Claims;
using Microsoft.Owin.Security;
using System.Security.Principal;
using duma.Infrastructure;
using System.Drawing.Imaging;

namespace duma.Controllers
{
    public class AccountController : Controller {
        //===========================================================
        /*Вначале определяется новое свойство, представляющее объект IAuthenticationManager,
            с помощью которого мы будем управлять входом на сайт.Для этого интерфейс IAuthenticationManager определяет два метода:
            SignIn(): создает аутентификационные куки 
            SignOut(): удаляет аутентификационные куки
            В Get-версии метода Login мы получаем адрес для возврата и передаем его в преставление.
            В Post-версии метода Login получаем данные из представления в виде модели LoginModel и по ним пытаемся получить 
            пользователя из бд с помощью метода UserManager.FindAsync(model.Email, model.Password). 
            Этот метод возвращает объект ApplicationUser в случае успеха поиска.
            AspNet Identity использует аутентификацию на основе объектов claim.
            Поэтому нам надо сначала создать объект ClaimsIdentity, который представляет реализацию интерфейса IIdentity
            в AspNet Identity. Для создания ClaimsIdentity применяется метод CreateIdentityAsync()
            И на финальном этапе вызывается метод AuthenticationManager.SignIn(),
            в который передается объект конфигурации аутентификации AuthenticationProperties, 
            а также ранее созданный объект ClaimsIdentity.Свойство IsPersistent позволяет сохранять аутентификационные данные
            в браузере даже после закрытия пользователем браузера.
            И метод Logout просто удаляет аутентификационные куки в браузере, как бы делая выход из системы.*/

        //===========================================================
        //===========================================================
        public ActionResult Captcha() {

            string code = new Random(DateTime.Now.Millisecond).Next(1111, 9999).ToString();
            Session["code"] = code;
            CaptchaImage captcha = new CaptchaImage(code, 110, 50);

            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";

            captcha.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);

            captcha.Dispose();

            return null;
        }
        //===========================================================
        private ApplicationUserManager UserManager {
            get {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }
        //===========================================================
        //Вначале определяется новое свойство, представляющее объект IAuthenticationManager, с помощью которого мы будем управлять входом на сайт
        private IAuthenticationManager AuthenticationManager {
            get {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        public ActionResult XEnter() {
            return View("login");
        }
        //===========================================================
        [HttpGet]
        public ActionResult Delete() {
            return View();
        }
        //===========================================================
        //Методы Delete и DeleteConfirmed отображают пользователю представление для удаления и принимают выбор
        //пользователя об удалении. Для удаления используется метод UserManager.DeleteAsync(). 
        //Он возвращает объект IdentityResult, который позволяет отследить успех операции.
        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed() {
            ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (user != null) {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded) {
                    return RedirectToAction("Logout", "Account");
                }
            }
            return RedirectToAction("Index", "Home");
        }
        //===========================================================
        public async Task<ActionResult> Edit() {
            ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (user != null) {
                EditModel model = new EditModel { Year = user.Year };
                return View(model);
            }
            return RedirectToAction("Login", "Account");
        }
        //===========================================================
        //В данном случае мы редактируем только значение свойства Year. 
        //POST-версия метода принимает данные модели и устанавливает значения ее свойств для пользователя. 
        //Редактирование также производится одним методом - методом UserManager.UpdateAsync()
        [HttpPost]
        public async Task<ActionResult> Edit(EditModel model) {
            ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (user != null) {
                user.Year = model.Year;
                IdentityResult result = await UserManager.UpdateAsync(user);
                if (result.Succeeded) {
                    return RedirectToAction("Index", "Home");
                }
                else {
                    ModelState.AddModelError("", "Что-то пошло не так");
                }
            }
            else {
                ModelState.AddModelError("", "Пользователь не найден");
            }

            return View(model);
        }
        //===========================================================
        [HttpGet]
        public ActionResult Login() {

            var hostname = HttpContext.Request.IsLocal;//$(window.location).attr('hostname');

            var url = hostname ? "/UserData/Parameter/" : "/duma/UserData/Parameter/";

            //ViewBag.returnUrl = "/DumaA/UserData/Parameter/";
            ViewBag.returnUrl = url;
            //var model = new LoginModel { Email = "xxx@yyy", Password = "123" };
            return PartialView();
        }
        //===========================================================        
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl) {

            if (ModelState.IsValid) {

                ApplicationUser user = await UserManager.FindAsync(model.Email, model.Password);

                if (user == null) {

                    ModelState.AddModelError("", "Неверный логин или пароль.");

                }

                else if (user.EmailConfirmed == true && model.Captcha == (string)Session["code"]) {

                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user,
                    DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();//удаляет аутентификационные куки
                    AuthenticationManager.SignIn(new AuthenticationProperties //создает аутентификационные куки

                    {
                        //Свойство IsPersistent позволяет сохранять аутентификационные данные в браузере даже после закрытия пользователем браузера.
                        IsPersistent = true
                    }, claim);

                    if (!String.IsNullOrEmpty(returnUrl)) {
                        string macadress = Request.Form["macadress"];
                        Session["macadress"] = macadress;
                        return Redirect(returnUrl);
                    }
                    //return Redirect("/UserData/Parameter");
                    //return Redirect("/UserData/XmlParameter");                    
                }
            }
            //var hostname = HttpContext.Request.IsLocal;//$(window.location).attr('hostname');

            //var url = hostname ? "/Home/Index" : "/duma/Home/Index";
            
            return View();
        }
        //===========================================================
        //И метод Logout просто удаляет аутентификационные куки в браузере, как бы делая выход из системы.
        public ActionResult Logout() {

            AuthenticationManager.SignOut();

            return RedirectToAction("Login");
        }
        //===========================================================
        public ActionResult Register() {
            //if(User.Identity.IsAuthenticated) return View();
            //return Redirect("/Account/Login/");
            return View();
        }
        //===========================================================
        //Первым делом в контроллере создается свойство UserManager, возвращающее объект ApplicationUserManager. 
        //Через него мы будем взаимодействовать с хранилищем пользователей. Для получения хранилища применяется выражение
        //HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>()
        //Если создание пользователя прошло успешно, то его свойство Succeeded будет равно true.
        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model) {
            if (ModelState.IsValid) {
                ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email, Year = model.Year };
                //Представляет собой результат операции идентификации
                //Если создание пользователя прошло успешно, то его свойство Succeeded будет равно true.
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded) {
                    // генерируем токен для подтверждения регистрации
                    var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // создаем ссылку для подтверждения
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code },
                               protocol: Request.Url.Scheme);
                    // отправка письма
                    await UserManager.SendEmailAsync(user.Id, "Подтверждение электронной почты",
                               "Для завершения регистрации перейдите по ссылке:: <a href=\""
                                                               + callbackUrl + "\">завершить регистрацию</a>");
                    return View("DisplayEmail");
                    //return RedirectToAction("Login", "Account");
                }
                else {
                    foreach (string error in result.Errors) {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }
        //===========================================================
        public ActionResult Account() {
            if (User.Identity.IsAuthenticated) {
                return View(new ApplicationUser { UserName = User.Identity.Name });
            }
            return RedirectToAction("Login");
        }
        //===========================================================
        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code) {
            if (userId == null || code == null) {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);// mysql нет тип данных bool, пока 0,1 = true
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }
    }
}