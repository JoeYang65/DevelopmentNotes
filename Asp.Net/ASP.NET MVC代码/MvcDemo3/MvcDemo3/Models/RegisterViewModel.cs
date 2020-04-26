using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcDemo3.Models
{
    public class RegisterViewModel
    {
        //通过输入prop，然后按两次tab键，自动生成属性的代码
        [Required(ErrorMessage = "邮箱地址必须填")]
        [RegularExpression("正则表达式内容",ErrorMessage ="邮箱地址有误")]//自定义格式验证，通过正则表达式
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]//数据类型
        public string Password { get; set; }//字符串是引用类型，默认是选填
        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPasword { get; set; }

        [Display(Name ="年龄")]
        [Range(10,20,ErrorMessage ="年龄必须在10到20之间")]
        public int Age { get; set; }
        public DateTime? BornDate { get; set; } //加了问号表示选填，如果是int,double,char,DateTime这种值类型的，默认是必填项
    }
}