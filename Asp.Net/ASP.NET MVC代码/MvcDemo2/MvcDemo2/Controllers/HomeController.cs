using MvcDemo2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcDemo2.Controllers
{
    //将数据从控制器(Controller)传向视图（View)
    //1.ViewBag     2.ViewData      3.TempData      4.model

    public class HomeController : Controller
    {
        // GET: Home action 在控制器里面每一个被访问的方法成为action
        public ActionResult Index()
        {
            //通过ViewBag在Controller和View之间传递数据
            //不能跨页面
            //一般存放一些不是主要的数据
            ViewBag.Content = "这是Controller里的数据";//通过属性的方式写
            ViewBag.MyName = "Joe";
            ViewBag.MyAge = "23";

            //通过ViewData，ViewData和ViewBag存储的本质一样，两者只是两种写法而以，数据都可访问
            //不能跨页面
            ViewData["Age"] = 30;//通过键值对的方式写
            
            return View();
        }

        public void Demo()
        {
            //TempData，存储之后，只能读取一次然后就被清空，类似于只能读取一次的Session
            TempData["Hello"] = "world";//赋值
            Response.Redirect("~/Home/Demo2");
        }
        public ActionResult Demo2()
        {
            return View();//在Demo2的View里面获取TempData数据，获取之后就没有了
        }

        public ActionResult ShowData()
        {
            //View方法有多个重载，根据参数不同调用不同方法，参数有viewName视图页名称，masterName布局页名称，model数据模型
            return View(new Student()
            {
                Id = 1,
                Name="张三",
                Age=20
            });
        }
    }
}