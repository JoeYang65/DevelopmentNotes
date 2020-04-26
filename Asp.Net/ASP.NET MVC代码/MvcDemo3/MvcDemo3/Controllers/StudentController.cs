using MvcDemo3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcDemo3.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return Content("Student_Index");
        }

        public ActionResult List()
        {
            return View(new List<Student>
            {
                new Student()
                {
                    Id=1,
                    Name="张三",
                    Age=12,
                },
                new Student()
                {
                    Id=2,
                    Name="张三2",
                    Age=12,
                },
                new Student()
                {
                    Id=3,
                    Name="张三3",
                    Age=12,
                }
            });
        }
    }
}