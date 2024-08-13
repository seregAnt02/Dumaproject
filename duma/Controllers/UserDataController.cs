using duma.Infrastructure;
using duma.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace duma.Controllers
{
    public class UserDataController : Controller {
        IRespository db;
        public UserDataController(IRespository repo) {
            db = repo;
        }

        //--------------------------------------------
        //--------------------------------------------
       
        public XDocument XmlParameter(int id = 1) {
            //ApplicationUser user = await UserManager.FindAsync(model.Email, model.Password);
            if (User.Identity.IsAuthenticated) {
                var model = db.Context.Parameters.Where(x => x.id == id).FirstOrDefault();
                var duma = db.Context.Dumas.Where(x => x.id == id).FirstOrDefault();
                model.Duma = duma;
                return InsertXmlParameter(model);
            }
            return null;
        }
        //--------------------------------------------
        public ActionResult Video(int id = 1) {
            var model = db.Context.Dumas.Where(x => x.id == id).FirstOrDefault();
            return View(model);
        }
        //--------------------------------------------       
        // GET: UserData
        [HttpGet]
        public ActionResult Parameter(int id = 1) {

            if (User.Identity.IsAuthenticated) {

                var model = db.Context.Parameters.Where(x => x.id == id).FirstOrDefault();

                var duma = db.Context.Dumas.Where(x => x.id == id).FirstOrDefault();

                //var model = db.Context.Parameters.AsNoTracking().Include("Duma").ToList().Find(x => x.id == id);

                if (ModelState.IsValid) {

                    TimeSpan time = DateTime.Now.Subtract(model.datetime);
                    model.codparameter = User.Identity.IsAuthenticated.ToString();
                    model.lastupdate = time.ToString();
                    model.Duma = duma;

                }

                return View(model);
            }

            var hostname = HttpContext.Request.IsLocal;//$(window.location).attr('hostname');

            var url = hostname ? "/Account/Login" : "/duma/Account/Login";

            //return Redirect("/DumaA/Account/Login/");
            return Redirect(url);
        }
        //--------------------------------------------
        [HttpPost]
        public string Parameter(Parameter form_model) {

            if (User.Identity.IsAuthenticated && ModelState.IsValid) {

                try {
                    //var model_parameter = db.Context.Parameters.AsNoTracking().Include("Duma").ToList().Find(x => x.id == model.id);
                    var model = db.Context.Parameters.Where(x => x.id == form_model.id).FirstOrDefault();
                    //var duma = db.Context.Dumas.Where(x => x.id == form_model.id).FirstOrDefault();                    
                    //выполнить комаду UPDATE для обновления данных в БД                                                                                                                   
                    model.datetime = DateTime.Now;
                    model.meaning = form_model.meaning;
                    model.lastupdate = form_model.lastupdate;
                    db.Context.Entry(model).State = EntityState.Modified;
                    var status = db.Context.Entry(model).State;
                    model.Duma.status = status.ToString();
                    db.Context.SaveChanges();
                    //db.AddOrUpdateOrSave(model); 
                    return "редактирование параметров";

                }
                catch (Exception ex) {

                    HttpContext.Response.Redirect("/DumaA/UserData/Parameter/" + form_model.id);
                    return ex.Message;
                    //HttpContext.Response.Write(ex.Message);                    
                }
            }
            return null;
        }
        //--------------------------------------------
        private XDocument InsertXmlParameter(Parameter model) {
            if (model != null) {
                XDocument xdoc = new XDocument();
                // создаем первый элемент
                XElement dum = new XElement("parameter");
                // создаем атрибут            
                XElement IdElement = new XElement("id", model.id);
                XElement DateTimeElement = new XElement("datetime", model.datetime);
                XElement parameterElem = new XElement("parameter", model.parameter);
                XElement codParameterElement = new XElement("codparameter", model.codparameter);
                XElement lastupdateElement = new XElement("lastupdate", model.lastupdate);
                XElement meaningElement = new XElement("meaning", model.meaning);
                XElement idElement = new XElement("dumaId", model.DumaId);
                XElement dumaIdElement = new XElement("duma.id", model.Duma.id);
                XElement dumaGuIdElement = new XElement("duma.guId", model.Duma.guId);
                XElement dumaDatetimeElement = new XElement("duma.datetime", model.Duma.datetime);
                XElement dumaMacadressElement = new XElement("duma.macadress", model.Duma.macadress);
                XElement dumaIpadressElement = new XElement("duma.ipadress", model.Duma.ipadress);
                XElement dumaPortElement = new XElement("duma.port", model.Duma.port);
                XElement dumaStatusElement = new XElement("duma.status", model.Duma.status);
                XElement dumaNumberElement = new XElement("duma.number", model.Duma.number);
                XElement dumaMigrationElement = new XElement("duma.migration", model.Duma.migration);
                XElement dumaAgeElement = new XElement("duma.age", model.Duma.age);
                // добавляем атрибут и элементы в первый элемент            
                dum.Add(IdElement);
                dum.Add(DateTimeElement);
                dum.Add(parameterElem);
                dum.Add(codParameterElement);
                dum.Add(lastupdateElement);
                dum.Add(meaningElement);
                dum.Add(idElement);
                dum.Add(dumaIdElement);
                dum.Add(dumaGuIdElement);
                dum.Add(dumaDatetimeElement);
                dum.Add(dumaMacadressElement);
                dum.Add(dumaIpadressElement);
                dum.Add(dumaPortElement);
                dum.Add(dumaStatusElement);
                dum.Add(dumaNumberElement);
                dum.Add(dumaMigrationElement);
                dum.Add(dumaAgeElement);
                // создаем корневой элемент
                XElement dums = new XElement("parameters");
                // добавляем в корневой элемент
                dums.Add(dum);
                // добавляем корневой элемент в документ
                xdoc.Add(dums);
                //сохраняем документ
                //xdoc.Save("dums.xml");
                return xdoc;
            }
            return null;
        }
        //--------------------------------------------
        XDocument TransformXslt() {
            XslCompiledTransform transform = null;
            XDocument transformedDoc = new XDocument();
            using (XmlWriter xmlWriter = transformedDoc.CreateWriter()) {
                transform = new XslCompiledTransform();
                string urlXslt = Request.PhysicalApplicationPath + @"Resurce\XSLTParameter.xslt";
                string urlXml = Request.PhysicalApplicationPath + @"Resurce\XMLParameter.xml";
                XPathDocument xpathDoc = new XPathDocument(urlXml);
                //StringReader strRead = new StringReader(xsl);                                 
                //string xsl = GetFileStream();
                transform.Load(XmlReader.Create(urlXslt));
                transform.Transform(xpathDoc, null, xmlWriter, null);
                //transform.Load(XmlReader.Create(new StringReader(xsl)));                
            }
            return transformedDoc;
        }
        //--------------------------------------------
        string GetFileStream() {
            string xsl = null;
            try {
                string url = Request.PhysicalApplicationPath + @"Resurce\XSLTParameter.xslt";
                using (FileStream fs = new FileStream(url, FileMode.Open)) {
                    byte[] b = new byte[1024];
                    UTF8Encoding temp = new UTF8Encoding(true);
                    while (fs.Read(b, 0, b.Length) > 0) {
                        xsl = temp.GetString(b);
                        Console.WriteLine(xsl);
                    }
                }
            }
            catch (Exception ex) {
                xsl = ex.Message;
            }
            return xsl;
        }
        //--------------------------------------------
        [HttpGet]
        public ActionResult JsonSearch(int id = 1) {
            var model = db.GetAsyncModelParameter(id);
            return View(model);
        }
        //--------------------------------------------
        [HttpPost]
        public JsonResult JsonSearch(string name, Parameter model) {
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        //--------------------------------------------
        XDocument XmlDum(Duma model) {
            XDocument xdoc = new XDocument();
            // создаем первый элемент
            XElement dum = new XElement("dum");
            // создаем атрибут
            XAttribute dumNameAttrib = new XAttribute("ip", model.ipadress);
            XElement dumIdElem = new XElement("id", model.id);
            XElement dumMacElem = new XElement("data", model.datetime);
            XElement dumStatusEl = new XElement("status", model.status);
            // добавляем атрибут и элементы в первый элемент
            dum.Add(dumNameAttrib);
            dum.Add(dumIdElem);
            dum.Add(dumMacElem);
            dum.Add(dumStatusEl);
            // создаем корневой элемент
            XElement dums = new XElement("dums");
            // добавляем в корневой элемент
            dums.Add(dum);
            // добавляем корневой элемент в документ
            xdoc.Add(dums);
            //сохраняем документ
            //xdoc.Save("dums.xml");
            return xdoc;
        }
        //--------------------------------------------                
        void InputRef(int id = 1) {
            if (User.Identity.IsAuthenticated) {
                var model = db.Context.Parameters.Where(x => x.id == id).FirstOrDefault();
                var duma = db.Context.Dumas.Where(x => x.id == id).FirstOrDefault();
                model.Duma = duma;

                string mac_adress = Session["macadress"].ToString();
                if (mac_adress != model.Duma.macadress && model.Duma.macadress == mac_adress) {
                    model.Duma.ipadress = HttpContext.Request.UserHostAddress;
                    //model.Duma.port = int.Parse(HttpContext.Response.Headers["port"].ToString());

                    //сохранение soceta
                    db.Context.Entry(model).State = EntityState.Modified;
                    var status = db.Context.Entry(model).State;
                    model.Duma.status = status.ToString();
                    db.Context.SaveChanges();
                }
            }
        }
        //--------------------------------------------         
        public ActionResult Audio() {
            return View();
        }
        //--------------------------------------------        
        public ActionResult Dash() {
            if (User.Identity.IsAuthenticated) ViewBag.IsAuthenticated = "true";
            return View();
        }
        //--------------------------------------------
        public ActionResult ChartArrayBasic() {
            return View();
        }
        //--------------------------------------------
        public ActionResult VideoDash() {
            if (User.Identity.IsAuthenticated) ViewBag.IsAuthenticated = "true";
            return View();
        }
        //--------------------------------------------
    }
}