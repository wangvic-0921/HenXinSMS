using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Web.Services;
using System.Data;
namespace HengXinSMS.Models
{
    public class MemberCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string loginuser = (string)System.Web.HttpContext.Current.Session["UserName"];
            if (loginuser==null)
            {
                //跳转到登录页面
                filterContext.HttpContext.Response.Redirect("/Login/login");

              
            }
            else
            {
                base.OnActionExecuting(filterContext);
           
            }
        }
    }
}