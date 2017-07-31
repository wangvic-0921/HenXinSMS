using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.ComponentModel;
using HengXinSMS.Models;
namespace HengXinSMS.Controllers
{
    public class ChangPwdController : Controller
    {
       

        public ActionResult changepwd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult updatepwd()
        {
            CheckRepeat c = new CheckRepeat();
            string _username = Request["UserName"];
            string _password = Request["PassWord"];
            string _newpassword = Request["NewPassWord"];
            int flag = c.update_pwd(_username, _password, _newpassword);
           
            if(flag==0)
            {
                return Content("Error");
            }
            else
            {
                return Content("Success");
            }
        }
    }
}
