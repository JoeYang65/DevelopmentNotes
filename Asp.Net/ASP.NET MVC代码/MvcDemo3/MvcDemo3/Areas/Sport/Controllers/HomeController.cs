using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcDemo3.Areas.Sport.Controllers
{
    public class HomeController : Controller
    {
        // GET: Sport/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}