# ASP.NET MVC笔记

## Http的内置对象：

（对应项目例子名称-MvcDemo1，项目启动后可通过Home/xxx访问action，Post请求用到的html页面叫HtmlPage1，直接选中后右键浏览器打开，修改C#代码后需要生成解决方案后才能生效。）



| Request                      | Response                           | Session                                                      | Cookie                                                     | Application                                                  | Server                                                       |
| ---------------------------- | ---------------------------------- | ------------------------------------------------------------ | ---------------------------------------------------------- | ------------------------------------------------------------ | ------------------------------------------------------------ |
| 请求                         | 响应                               | 会话                                                         | 客户端数据                                                 | 当前网站对象                                                 | 服务器对象                                                   |
| 服务器接收客户端数据         | Response.Write    向客户端输出内容 | 数据保存在服务器中，存储少量重要数据比如账号信息             | Cookies.Add(new HttpCookie("token"){ })                    | 是整个项目（网站）共有的数据，<br/>每个用户都能访问          | Transfer转发： 路径不变，内容变化，只能转发站内的内容<br/>重定向： 路径改变，内容没有限制 |
| Request.QueryString——get请求 | Response.Redirect 重定向           | Session 是一个键值对（key - value）                          | Cookies["token"].Value                                     | HttpContext.Application["user"] = "123";                     | Server.Transfer("~/xxx.aspx");                               |
| Request.Form——post请求       | Response.Headers响应头             | 1-安全性比cookie强<br/>2-但占用服务器内存，在内存一定的情况下，每个用户数据占用空间越大则服务器能并发的人越少 | 清除cookie值。通过使cookie过期的方式让浏览器（客户端）清除 | Content(<br/>HttpContext.Application["user"].ToString()<br/>); | Response.Redirect("http://www.baidu.com");                   |



## 控制器和视图之间数据传递

（对应项目例子-MvcDemo2）

**将数据从控制器(Controller)传向视图（View)**

（对应控制器HomeController）

​    1.ViewBag     2.ViewData      3.TempData      4.model

| ViewBag                                   | ViewData                                  | TempData                                                     | model                                                        |
| ----------------------------------------- | ----------------------------------------- | ------------------------------------------------------------ | ------------------------------------------------------------ |
| 不能跨页面<br/>一般存放一些不是主要的数据 | 不能跨页面<br/>一般存放一些不是主要的数据 | 存储之后，只能读取一次然后就被清空，类似于只能读取一次的Session | View方法有多个重载，根据参数不同调用不同方法，参数有viewName视图页名称，masterName布局页名称，model数据模型 |
| ViewBag.MyName = "Joe";                   | ViewData["Age"] = 30;                     | TempData["Hello"] = "world";//赋值                           | View(new Student()<br/>{<br/>Id = 1,<br/>Name="张三",<br/> Age=20<br/> }) |



**视图传数据到控制器**

（对应控制器DemoController，对应action：Index()，ShowForm() ）

| Get                                  | Post                                 |
| ------------------------------------ | ------------------------------------ |
| action方法接受参数Index(string name) | Action方法接受参数Index(string name) |
| 地址栏url加 "?name=value"            | Form表单提交                         |
| 相当于Request.QueryString["name"]    | Request.Form["name"]                 |

action方法除了声明各个参数接受请求提交的数据以外，可以直接使用模型。

如 public ActionResult **Login**(Models.LoginViewModel model){ }

只要数据提交的变量名称与模型的属性名称对应就行，这样还能先对上传的数据进行验证，判断数据是否符合模型属性的定义



## 通过ViewModel自动生成View页面

（对应控制器DemoController，对应action：Login() ）

1. 在Models目录建立LoginViewModel.cs，这是登录所需的数据模型，设置好需要的属性，如name，password；
2. 在DemoController建立Login的action方法，可分成只接受Get请求和只接受Post请求的action方法，共两个Login的action方法；
3. 根据LoginViewMode自动生成view（cshtml页面），在Login方法名上右键，点击添加视图，选择模板（Template）为Create，模型类（Model class）为LoginViewModel，选项（Options）勾选引用脚步库（Reference script libraries）和使用布局页（Use a layout page），点击添加（Add）。

这样生成的view页面会自动拥有模型属性所需数据的**form表单**，而且会根据模型属性的注解**自动验证**，根据需要可修改是否必填、显示名称、字符长度、错误信息提醒内容……

后期，如果模型修改，view页面也可以再重新生成一次。

注：创建视图view时之所以选择模板（Template）Create，是因为Create模板需要先填写数据再向服务器发送一次请求来创建新记录到数据库，和登录的情况比较类似。



## ActionResult下面可用的子类

（对应项目：MvcDemo3，对应控制器HomeController）

ActionResult子类：

​		ViewResult类——返回相应的视图View

​		ContentResult类——返回字符串

​		RedirectResult类——重定向到url地址 Redirect("http://www.baidu.com");

​		RedirectToRouteResult类——根据路由进行重定向 RedirectToAction("参数1-actionName"，"参数2-controllerName")，如果没有参数2则跳转到当前控制器里面的action。

​		FileResult类——向客户端输出文件，File("参数1-文件路径","参数2-文件类型"); 有多种重载方发，这个最简单而以。

​		JsonResult类——向客户端返回对象的json序列化后的结果

​		HttpStatusCodeResult类——显示不同的http状态码信息

​		PartialViewResult类——返回 ”部分页面“



## Razor剃须刀 - 网页的模板

（对应项目：MvcDemo3，对应控制器StudentController，对应页面List.cshtml）



## Areas区域

区域可将项目分成不同模块/版块，各个模块自己有路由规则、控制器、视图、模型。

便于文件结构的管理，避免代码堆积在同一个文件里面。

**区域Area是独立的MVC，拥有独立的路由**

分了区域后的注意问题：

1、原本项目的路由 和 区域Areas里面的路由，如果有相同的controller名称，则会冲突，可以更改RouteConfig.cs里面的配置，为原来的路由加上namespace参数。

​	解决方法：在RouteConfig.cs文件defaults下面添加namespaces（如文件注释）

2、**跨路由**跳转，如MvcDemo3里面一共有3个路由，默认1个，Sport1个，Admin1个。自动生成<a>标签。

​	解决方法：在页面View里面用这种方式写链接`@Html.RouteLink`

​	**不跨路由**，Action之间跳转，自动生成<a>标签。

​	解决方法：用这种方式写链接`@Html.ActionLink`

3、`@Url.Action`和`@Url.RouteUrl`不会自动生成<a>标签，但会生成URL地址，第一个方法是生成本路由的路径，第二个方法是生成跨路由的路径。





