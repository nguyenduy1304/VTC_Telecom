using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace STORE.Controllers
{
    public class HomeController : Controller
    {
        //private string baselink = "https://localhost:44338/";
        private string baselink = "http://vtcsso.com/";
        public ActionResult Index()
        {
            return View();
        }
    }
}