using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace HengXinSMS.Models
{
    public class User
    {
       
        public int ID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string PassWord { get; set; }
        [Required]
        public string Depart { get; set; }
        [Required]
        public string Level { get; set; }
    }
    public class LoginModel
    {
        public string UserName { get; set; }            //用户名
        public string PassWord { get; set; }            //密码
    }
}