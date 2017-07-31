using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace HengXinSMS.Controllers
{
    public class SiteAboutDateController : Controller
    {
        //
        // GET: /SiteAboutDate/

        public ActionResult selectdate_1()
        {
            return View();
        }
         [HttpPost]
        public ActionResult send_date_1()
        {
            string _year = Request["Year"];
            string _month = Request["Month"];
            if (_year != null && _month != null)
            {
                this.TempData["Year"] = _year;
                this.TempData["Month"] = _month;
                return Content("ok:/SiteManagement/sitemanagement");

            }
            else
            {
                return Content("no:场地查询失败！");
            }
        }
    }
}
