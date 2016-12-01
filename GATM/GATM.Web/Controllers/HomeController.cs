using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GATM.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard(string cardNumber, string pin)
        {
            return View();
        }

        public ActionResult History()
        {
            return View();
        }
    }
}