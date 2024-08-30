using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using duma.Infrastructure;
using duma.Models;

namespace duma.Controllers {
    public class HomeController : Controller {
        IRespository db;

        public HomeController() { }

        public HomeController(IRespository repo) {
            /*
             * Чтобы управлять зависимостями через Ninject, вначале надо создать объект Ninject.IKernel 
             * с помощью встроенной реализации этого интерфейса - класса StandardKernel:
             */
            //IKernel ninjectKernel = new StandardKernel();
            /*
             * Далее нужно установить отношения между интерфейсами и их реализациями:
             */
            //ninjectKernel.Bind<IRespository>().To<MySqlRepository>();
            /*
             * Данное выражение указывает, что объекты IRepository должны будут рассматриваться как MySqlRepository.
                И в конце создается объект интерфейса через метод Get:
             */
            //db = ninjectKernel.Get<IRespository>();
            db = repo;
            /*
             * Несмотря на то, что в конструктор контроллера не передается никакой конкретной реализации, но все будет работать.
             *  Рассмотрим поэтапно, как происходит в данном случае внедрение зависимостей:                         
            Фреймворк MVC получает запрос и обращается к контроллеру HomeController
            Фреймворк MVC обращается к классу сопоставления зависимостей (в данном случае класс NinjectDependencyResolver), 
            чтобы тот создал новый объект HomeController, передавая параметр Type в метод GetService (в класс NinjectDependencyResolver)
            Сопоставитель зависимостей вызывает инфраструктуру Ninject для создания нового объекта HomeController, 
            передавая тип создаваемого объекта в метод TryGet
            Ninject смотрит на конструктор HomeController и видит, что там используется зависимость от интерфейса IRepository, 
            для которого он устанавливает сопоставление с конкретной реализацией
            Ninject создает экземпляр класса BookRepository и затем использует его для создания контроллера HomeController
            Ninject передает созданный объект HomeController сопоставителю зависимостей, который, в свою очередь, передает его фреймворку MVC.
            И далее происходит обработка запроса.


            Таким образом, мы избегаем использования конкретных реализаций и работаем с объектами на уровне интерфейсов.
            А сопоставление интерфейсов с конкретными реализациями перекладывается на класс NinjectDependencyResolver.
             */
        }

        //============================================     
        [HttpGet]
        public ActionResult Upload(int id) {

            return PartialView();
        }

        //============================================
        [HttpPost]
        public JsonResult Upload() {

            //var input = Request.Form["upload"];
            string path = null;
            foreach (string file in Request.Files) {

                var upload = Request.Files[file];

                if (upload != null) {

                    // получаем имя файла
                    string fileName = System.IO.Path.GetFileName(upload.FileName);

                    //считаем загруженный файл в массив
                    byte[] avatar = new byte[upload.ContentLength];

                    upload.InputStream.Read(avatar, 0, upload.ContentLength);

                    path = "~/Files/" + fileName;

                    // сохраняем файл в папку Files в проекте
                    upload.SaveAs(Server.MapPath(path));
                    
                }
            }
            return Json(path);
        }

        //============================================
        [HttpGet]
        public ActionResult ContactMessage(int content = 0) {


            ViewBag.ContentBool = content;

            return PartialView();
        }
        //============================================
        [HttpPost]
        public async Task<ActionResult> ContactMessage(Contact model, string content, string urlupload) {

            if (ModelState.IsValid) {                

                //EmailService emailService = new EmailService();

                EmailMessage emailMessage = new EmailMessage();

                string body = string.Format("Имя: {0} Телефон: {1} Почта: {2}\r\nКонтент: {3}", model.Name, model.Telephone, model.EmailAdress, content);
                

                string path = urlupload == "undefined" ? null : Server.MapPath(urlupload);// undefined

                var rezult = emailMessage.SendEmailAsync("s_antonov02@rambler.ru", model.EmailAdress, body, path);
                await rezult;

                try {

                    rezult.Wait();

                    if(path != null) {

                        System.IO.FileInfo fileInfo = new System.IO.FileInfo(path);

                        if (fileInfo.Exists) {

                            //System.IO.File.Delete(path);
                            fileInfo.Delete();
                        }
                    }                    

                    return PartialView("Download", model);
                }
                catch (Exception ex) {

                    ViewBag.Ex = ex;

                    return PartialView("DownloadExeption", model);

                }
            }

            return PartialView();
        }

        //============================================

        public ActionResult ServerAdministration() {


            return PartialView();
        }
        //============================================  

        public ActionResult ApplicationDevelopment() {

            return PartialView();
        }

        //============================================   

        public ActionResult FormPrice() {


            return PartialView();
        }

        //============================================   

        public ActionResult PriceData() {
            //var model = db.Dumas.ToList();
            return PartialView();
        }
        //============================================
        public ActionResult Coordinate() {
            return PartialView();
        }
        //============================================
        public ActionResult Index() {
            //var model = db.DumaList();
            if (User != null && User.Identity.IsAuthenticated) ViewBag.IsAuthenticated = "true";
            //var model = db.Dumas.Select(s => s.Date).FirstOrDefault();
            var result = PartialView();
            result.ViewName = "Index";
            ViewData["Message"] = "Hello world!";
            return result;
        }
        //============================================
        [HttpGet]
        public ActionResult Price() {
            if (User.Identity.IsAuthenticated) ViewBag.IsAuthenticated = "true";
            return PartialView(new Price { Area = 1, Rooms = 1 });
        }
        //============================================
        [HttpGet]
        public int Calculator(Price data) {

            var price = db.Context.TotalPrices.ToList();

            int lighting = 0; switch (data.Lighting) {

                case "16 Групп освещения":
                    lighting = 16;
                    break;
                case "32 Групп освещения":
                    lighting = 32;
                    break;
                case "64 Групп освещения":
                    lighting = 64;
                    break;
                default: lighting = 8; break;

            }

            var priceTotal = (data.Area * price[0].TotatalAreaHouse) + (data.Rooms * price[0].NumberRooms) + (lighting * price[0].Lighting);

            //return Json(priceTotal, JsonRequestBehavior.AllowGet);

            return priceTotal;
        }
        //============================================        
        [HttpPost]
        public ActionResult Price(Price model) {
            //model.Area = "999";            
            return PartialView();
        }
        //============================================
        public ActionResult Services() {
            return PartialView();
        }
        //============================================
        public ActionResult Company(string name) {
            return PartialView();
        }
        //============================================        
        [HttpGet]
        public ActionResult Contact(Price price, string pricetotal) {
            if (ModelState.IsValid) {
                ViewBag.PriceModel = price;
                //ViewBag.PriceTotal = pricetotal;
                return View();
            }
            return View("Price");
        }
        //============================================        
        [HttpPost]
        public async Task<ActionResult> Contact(Contact model, Price price, string urlupload) {

            if (ModelState.IsValid) {

                //EmailService emailService = new EmailService();
                string path = urlupload == "undefined" ? null : Server.MapPath(urlupload);// undefined

                EmailMessage emailMessage = new EmailMessage();

                string body = string.Format(@"Кому: {0} От кого: {1} Email: {2}
                                              Площадь(м2): {3} Комнат: {4}
                                              Освещение: {5} отопление: {6}
                                              вентиляция: {7} кондиционер: {8}
                                              датчики движения: {9} пожарные датчики: {10}
                                              управление входной группой: {11} протечки: {12}
                                              ваши пожелания: {13}",
                                              model.Name, model.Telephone, model.EmailAdress,
                                              price.Area, price.Area, price.Lighting, price.Heatings,
                                              price.Fan, price.Conditioner,
                                              price.Movements, price.Fireman,
                                              price.Management, price.Protection,
                                              model.Wishes);

                //await emailService.SendAsync("s_antonov02@rambler.ru", model.EmailAdress, body);

                await emailMessage.SendEmailAsync("s_antonov02@rambler.ru", model.EmailAdress, body, path);

                try {
                    return PartialView("Download", model);
                }
                catch (Exception ex) {
                    ViewBag.Ex = ex;
                    return PartialView("DownloadExeption");
                }
            }
            ViewBag.PriceModel = price;
            //ViewBag.PriceTotal = pricetotal;
            return View(model);
            //return Redirect("/duma/Home/Index");
        }
        //============================================
        [HttpGet]
        //[ActionName("Edit")]
        public ActionResult EditDuma(int id = 1) {
            if (User.Identity.IsAuthenticated) {
                var model = db.GetAsync(id);
                return View("EditDuma", model);
            }
            return Redirect("/Home/Index");
        }
        //============================================
        [HttpPost]
        public ActionResult EditDuma(Duma model) {
            //выполнить комаду UPDATE для обновления данных в БД
            model.datetime = DateTime.Now;
            //db.Context.Entry(model).State = System.Data.Entity.EntityState.Modified;
            //db.Save();
            return RedirectToAction("Index");
        }
        //============================================
        //============================================
    }
}