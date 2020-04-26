using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcDemo1.Controllers
{
    public class HomeController : Controller
    {
        //内置对象 Request      Response     Session      Cookie     Application     Server

        //Request 服务区接收客户端数据
        //Request.QueryString   get请求
        //Request.Form          post请求
        //Request.MapPath()将相对路径转化成绝对路径
        //Request.Files         post请求的文件（文件上传）

        // GET: Home
        public ActionResult Index()
        {
            return Content(Request.QueryString["name"]);
        }

        public ActionResult PostData()
        {
            return Content(Request.Form["loginname"]);
        }
        public ActionResult FileData()
        {
            //SavaAS方法需要绝对路径(物理路径）
            //Request.MapPath()将相对路径转化成绝对路径
            Request.Files["file"].SaveAs(Request.MapPath("~/uploads/" + Request.Files["file"].FileName));
            return Content("ok");
        }

        //Response服务器响应（回答）客户端
        public ActionResult ResponseData()
        {
            //Response.Write    向客户端输出内容
            //Response.Redirect 重定向

            //Response.Write("hello world");
            Response.Redirect("http://www.baidu.com");//重定向
            return Content("");
        }
        public ActionResult RequestHeader()
        {
            Response.Headers["hello"] = "world";//响应头，设置一个叫hello的关键字，它的值是world
            //请求传入一个叫token关键字到服务器，这里返回请求头里面的token关键字的值给客户端并显示在网页上
            return Content(Request.Headers["token"]);
        }

        //从用户用一个浏览器请求网站开始，这个用户就和网站建立了Session，无操作情况下一般保持20分钟，有操作情况下则从最后一次操作开始计时
        public ActionResult SessionData()
        {
            //Session 会话 - 数据保存在服务器中，存储少量重要数据比如账号信息
            //Session 是一个键值对（key - value）
            //1-安全性比cookie强
            //2-但占用服务器内存，在内存一定的情况下，每个用户数据占用空间越大则服务器能并发的人越少
            Session["user"] = Request.Form["user"];
            return Content("会话中的数据是：" + Session["user"]);
        }

        //清除Session数据
        public ActionResult ClearSession()
        {
            //Session.Clear();
            Session.Abandon();
            return Content("清空后，当前会话数据是：" + Session["user"]);
        }

        //Cookie
        public ActionResult CookieSave()//保存cookie数据
        {
            Response.Cookies.Add(new HttpCookie("token")//新建cookie的key名称
            {
                Value="abc123321cba",//token的值
                //cookie有效期,到期时间
                Expires=DateTime.Now.AddDays(7)
            });
            return Content("ok");
        }

        public ActionResult CookieGet()//访问cookie数据
        {
            return Content(Request.Cookies["token"].Value);
        }

        public ActionResult CookieClear()
        {
            //清除cookie值。通过使cookie过期的方式让浏览器（客户端）清除
            Response.Cookies.Add(new HttpCookie("token")
            {
                Expires = DateTime.Now.AddDays(-1)
            });
            return Content("ok");
        }

        //Session是每个用户数据相互独立，而Appliaction是整个项目（网站）共有的数据，每个用户都能访问。
        public ActionResult ApplicationData()
        {
            HttpContext.Application["user"] = "123";
            return Content("set user=123 in Application");
        }
        public ActionResult ApplicationGet()
        {
            return Content(HttpContext.Application["user"].ToString());
        }

        //Server
        public ActionResult ServerDemo()
        {
            //Transfer转发： 路径不变，内容变化，只能转发站内的内容
            //重定向： 路径改变，内容没有限制
            Server.Transfer("~/xxx.aspx");
            return Content("");
        }
        
    }
}