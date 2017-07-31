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
    public class SignUpController : Controller
    {
        //
        // GET: /SignUp/

        public ActionResult signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult adduser()
        {

            CheckRepeat c = new CheckRepeat();
            string _username = Request["UserName"];
            string _password = Request["PassWord"];
            string _depart = Request["Depart"];
            using (var context = new HenXinSMSContext())
            {
                int tag = c.search(_username);
                if (tag == 0)
                {
                    var user = new User { UserName = _username, PassWord = _password, Depart = _depart, Level ="项目成员"};
                    context.Users.Add(user);
                    context.SaveChanges();
                    int x = c.search(_username);
                    if (x == 1)
                    {
                        return Content("Success");
                    }
                    else
                    {
                        return Content("Error");
                    }
                }
                else
                {
                    return Content("Repeat");
                }
            }

        } 
    }
}
