using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcDemo2.Views
{
    public class DemoController : Controller
    {
        //[HttpGet]     //限制只接受Get请求
        // GET: Demo
        //通过参数的方式从View传入数据到Controller，Get或Post都能使Controller获取数据
        public ActionResult Index(string name)
        {
            //参数的方式相当于以下两种接收Get和Post的方式
            //Request.QueryString["name"] ——>Get
            //Request.Form["name"] ——>Post

            return Content(name);
        }
        public ActionResult ShowForm()
        {
            return View();
        }

        //处理登录请求
        [HttpPost]  //限制只接受Post请求
        //[ValidateAntiForgeryToken]//校验防伪令牌
        //通过参数接受数据-旧方式
        //public ActionResult Login(string name,string pwd)
        //{
        //    if (name == "admin" && password == "123")
        //    {
        //        return Content("OK");
        //    }
        //    else
        //    {
        //        return Content("NO");
        //    }
        //}
        public ActionResult Login(Models.LoginViewModel model)
        {
            //校验数据是否通过模型的数据注解，服务器端的校验
            if (ModelState.IsValid)//通过数据校验后执行
            {
                if (model.name == "admin" && model.password == "123456")
                {
                    return View();
                }
                else
                {
                    ModelState.AddModelError("", "账号密码有误");//返回到Login.cshtml的ValidationSummary（校验概要）显示
                    return View(model);
                }
            }
            ModelState.AddModelError("", "数据有误");
            return View(model);
        }
        //显示登录界面
        [HttpGet]   //限制只接受Get请求
        public ActionResult Login()
        {
            return View();
        }
    }
}