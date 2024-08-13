using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace duma.Controllers
{
    public class PolicyController : Controller
    {
        public ActionResult Soglasie()
        {
            return PartialView();
        }
        //--------------------------------------------
        public ActionResult PrivacyPolicy()
        {
            return PartialView();
        }

        //--------------------------------------------
        //[Authorize]
        public ActionResult Prikaz()
        {
            return PartialView();
        }
        //--------------------------------------------

        public ActionResult PrivacyPolicyList()
        {
            return PartialView();
        }
        //--------------------------------------------
        public ActionResult CookieInfo()
        {
            return PartialView();
        }
        //--------------------------------------------
    }
}