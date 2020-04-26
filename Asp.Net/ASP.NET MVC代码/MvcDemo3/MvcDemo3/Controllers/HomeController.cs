using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MvcDemo3.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Demo()
        {
            return Redirect("http://www.baidu.com");
        }

        public ActionResult DemoaAction()
        {
            return RedirectToAction("Index");
        }
        public ActionResult DemoAction2()
        {
            return RedirectToAction("Index", "Student");
        }

        public ActionResult GetFile()
        {
            return File(@"~/Images/Timeless_UHD.jpg","image/jpg");
        }

        private static string basePath = @"D:\";
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            var filename = DateTime.Now.Ticks + file.FileName;
            file.SaveAs(basePath + @"\" + filename);
            return Content(filename);
        }

        public ActionResult GetJson()
        {
            return Json(new{ id=1,name="张三"},JsonRequestBehavior.AllowGet);//将对象转换成Json后返回
        }

        public ActionResult GetCode()
        {
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);//返回http状态码
        }
        public PartialViewResult GetPartial()
        {
            //部分页面PartialView，不使用布局页
            //如果将PartialView改成View，PartialViewResult改成ActionResult，就会套用默认layout布局页
            return PartialView();
        }
    }
}