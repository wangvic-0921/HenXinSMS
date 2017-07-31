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
    public class LoginController : Controller
    {
   //get
     public ActionResult login()
        {
         /* using (var context = new HenXinSMSContext())
               {
                  var user = new User { UserName = "wuxunlei", PassWord = "wuxunlei", Depart="OML",Level ="管理员"};
                  context.Users.Add(user);
                  context.SaveChanges();
                
               }*/
            return View();
        }
     
        [HttpPost]
        public ActionResult Account()
        {
            using (var context=new HenXinSMSContext())
            {
                string _username = Request["UserName"];
                string  _password= Request["PassWord"];
                var _users = context.Users;
                CheckRepeat c = new CheckRepeat();
                int tag = c.verify(_username,_password);
                if(tag==0)
                {
                        return Content("no:用户名或密码不正确！");
                }
                else if(tag==1)
                {
                        var _user = _users.First(d => d.UserName == _username);
                        Session["UserName"] = _username;
                        Session["UserName"] = _user.UserName;
                        Session["Depart"] = _user.Depart;
                        Session["Level"] = _user.Level;
                        Session["ProjectName"] = "";
                        Session["ProjectId"] = "";
                        Session["ProjectManager"]="";
                        if (_user.Level == "管理员")
                        {
                            return Content("ok:/Home/home");
                          
                        }
                        else if (_user.Level == "项目经理")
                        {
                            return Content("ok:/Home_PM/home"); 
                        }
                        else if(_user.Level=="项目成员")
                        {
                            return Content("ok:/Home_EMPLOYEE/home"); 
                        }
                        else if (_user.Level == "全权阅读")
                        {
                            return Content("ok:/Home_GUEST/home");
                        }
                  }
                  else
                  {
                        return Content("no:密码错误！");
                  }
               
            }
            return Content("no:验证失败请联系管理员！");
        }
      
      }  
}
