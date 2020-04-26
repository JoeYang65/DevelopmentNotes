using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcDemo2.Models
{
    public class LoginViewModel
    {
        [EmailAddress]
        public string email { get; set; }

        [Display(Name ="用户名")]//View页面该模型属性显示的样式
        //数据注解
        [Required(ErrorMessage ="请填写用户名"),StringLength(10,MinimumLength =2)]
        public string name { get; set; }

        [Display(Name ="密码")]
        [DataType(DataType.Password)]
        [Required,MinLength(6)]
        public string password { get; set; }
    }
}