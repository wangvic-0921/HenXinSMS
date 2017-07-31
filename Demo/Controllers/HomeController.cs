using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.ComponentModel;
using HengXinSMS.Models;
using calculate;
namespace HengXinSMS.Controllers
{
    [MemberCheck]
    public class HomeController : Controller
    {
      
        public ActionResult home()
        {
            string tomorrow = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            DateTime date = Convert.ToDateTime(tomorrow);
            Dbhelper dbhelper = new Dbhelper();
            dbhelper.count("YSW", "Active", "Active",date);
            ViewBag.Message_Forklifts  = dbhelper.sum[0];
            ViewBag.Message_Packagings = dbhelper.sum[1];
            ViewBag.Message_Transports = dbhelper.sum[2];
            ViewBag.Tomorrow = tomorrow;
            ViewBag.Forklift_7  = dbhelper.timeOfForklifts[0];
            ViewBag.Forklift_8  = dbhelper.timeOfForklifts[1];
            ViewBag.Forklift_9  = dbhelper.timeOfForklifts[2];
            ViewBag.Forklift_10 = dbhelper.timeOfForklifts[3];
            ViewBag.Forklift_11 = dbhelper.timeOfForklifts[4];
            ViewBag.Forklift_12 = dbhelper.timeOfForklifts[5];
            ViewBag.Forklift_13 = dbhelper.timeOfForklifts[6];
            ViewBag.Forklift_14 = dbhelper.timeOfForklifts[7];
            ViewBag.Forklift_15 = dbhelper.timeOfForklifts[8];
            ViewBag.Forklift_16 = dbhelper.timeOfForklifts[9];
            ViewBag.Forklift_17 = dbhelper.timeOfForklifts[10];
            ViewBag.Packaging_7 = dbhelper.timeofPackagings[0];
            ViewBag.Packaging_8 = dbhelper.timeofPackagings[1];
            ViewBag.Packaging_9 = dbhelper.timeofPackagings[2];
            ViewBag.Packaging_10 = dbhelper.timeofPackagings[3];
            ViewBag.Packaging_11 = dbhelper.timeofPackagings[4];
            ViewBag.Packaging_12 = dbhelper.timeofPackagings[5];
            ViewBag.Packaging_13 = dbhelper.timeofPackagings[6];
            ViewBag.Packaging_14 = dbhelper.timeofPackagings[7];
            ViewBag.Packaging_15 = dbhelper.timeofPackagings[8];
            ViewBag.Packaging_16 = dbhelper.timeofPackagings[9];
            ViewBag.Packaging_17 = dbhelper.timeofPackagings[10];
            ViewBag.Transport_7 = dbhelper.timeofTransports[0];
            ViewBag.Transport_8 = dbhelper.timeofTransports[1];
            ViewBag.Transport_9 = dbhelper.timeofTransports[2];
            ViewBag.Transport_10 = dbhelper.timeofTransports[3];
            ViewBag.Transport_11 = dbhelper.timeofTransports[4];
            ViewBag.Transport_12 = dbhelper.timeofTransports[5];
            ViewBag.Transport_13 = dbhelper.timeofTransports[6];
            ViewBag.Transport_14 = dbhelper.timeofTransports[7];
            ViewBag.Transport_15 = dbhelper.timeofTransports[8];
            ViewBag.Transport_16 = dbhelper.timeofTransports[9];
            ViewBag.Transport_17 = dbhelper.timeofTransports[10];
            return View();   
        }
        public ActionResult home_1()
        {
            string tomorrow = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            DateTime date = Convert.ToDateTime(tomorrow);
            Dbhelper dbhelper = new Dbhelper();
            dbhelper.count("YWP", "Active", "Active", date);
            ViewBag.Message_Forklifts = dbhelper.sum[0];
            ViewBag.Message_Packagings = dbhelper.sum[1];
            ViewBag.Message_Transports = dbhelper.sum[2];
            ViewBag.Tomorrow = tomorrow;
            ViewBag.Forklift_7 = dbhelper.timeOfForklifts[0];
            ViewBag.Forklift_8 = dbhelper.timeOfForklifts[1];
            ViewBag.Forklift_9 = dbhelper.timeOfForklifts[2];
            ViewBag.Forklift_10 = dbhelper.timeOfForklifts[3];
            ViewBag.Forklift_11 = dbhelper.timeOfForklifts[4];
            ViewBag.Forklift_12 = dbhelper.timeOfForklifts[5];
            ViewBag.Forklift_13 = dbhelper.timeOfForklifts[6];
            ViewBag.Forklift_14 = dbhelper.timeOfForklifts[7];
            ViewBag.Forklift_15 = dbhelper.timeOfForklifts[8];
            ViewBag.Forklift_16 = dbhelper.timeOfForklifts[9];
            ViewBag.Forklift_17 = dbhelper.timeOfForklifts[10];
            ViewBag.Packaging_7 = dbhelper.timeofPackagings[0];
            ViewBag.Packaging_8 = dbhelper.timeofPackagings[1];
            ViewBag.Packaging_9 = dbhelper.timeofPackagings[2];
            ViewBag.Packaging_10 = dbhelper.timeofPackagings[3];
            ViewBag.Packaging_11 = dbhelper.timeofPackagings[4];
            ViewBag.Packaging_12 = dbhelper.timeofPackagings[5];
            ViewBag.Packaging_13 = dbhelper.timeofPackagings[6];
            ViewBag.Packaging_14 = dbhelper.timeofPackagings[7];
            ViewBag.Packaging_15 = dbhelper.timeofPackagings[8];
            ViewBag.Packaging_16 = dbhelper.timeofPackagings[9];
            ViewBag.Packaging_17 = dbhelper.timeofPackagings[10];
            ViewBag.Transport_7 = dbhelper.timeofTransports[0];
            ViewBag.Transport_8 = dbhelper.timeofTransports[1];
            ViewBag.Transport_9 = dbhelper.timeofTransports[2];
            ViewBag.Transport_10 = dbhelper.timeofTransports[3];
            ViewBag.Transport_11 = dbhelper.timeofTransports[4];
            ViewBag.Transport_12 = dbhelper.timeofTransports[5];
            ViewBag.Transport_13 = dbhelper.timeofTransports[6];
            ViewBag.Transport_14 = dbhelper.timeofTransports[7];
            ViewBag.Transport_15 = dbhelper.timeofTransports[8];
            ViewBag.Transport_16 = dbhelper.timeofTransports[9];
            ViewBag.Transport_17 = dbhelper.timeofTransports[10];
            return View();
        }
        public ActionResult home_third()
        {
            string tomorrow = DateTime.Now.AddDays(2).ToString("yyyy-MM-dd");
            DateTime date = Convert.ToDateTime(tomorrow);
            Dbhelper dbhelper = new Dbhelper();
            dbhelper.count("YSW", "Active", "Active", date);
            ViewBag.Message_Forklifts = dbhelper.sum[0];
            ViewBag.Message_Packagings = dbhelper.sum[1];
            ViewBag.Message_Transports = dbhelper.sum[2];
            ViewBag.Tomorrow = tomorrow;
            ViewBag.Forklift_7 = dbhelper.timeOfForklifts[0];
            ViewBag.Forklift_8 = dbhelper.timeOfForklifts[1];
            ViewBag.Forklift_9 = dbhelper.timeOfForklifts[2];
            ViewBag.Forklift_10 = dbhelper.timeOfForklifts[3];
            ViewBag.Forklift_11 = dbhelper.timeOfForklifts[4];
            ViewBag.Forklift_12 = dbhelper.timeOfForklifts[5];
            ViewBag.Forklift_13 = dbhelper.timeOfForklifts[6];
            ViewBag.Forklift_14 = dbhelper.timeOfForklifts[7];
            ViewBag.Forklift_15 = dbhelper.timeOfForklifts[8];
            ViewBag.Forklift_16 = dbhelper.timeOfForklifts[9];
            ViewBag.Forklift_17 = dbhelper.timeOfForklifts[10];
            ViewBag.Packaging_7 = dbhelper.timeofPackagings[0];
            ViewBag.Packaging_8 = dbhelper.timeofPackagings[1];
            ViewBag.Packaging_9 = dbhelper.timeofPackagings[2];
            ViewBag.Packaging_10 = dbhelper.timeofPackagings[3];
            ViewBag.Packaging_11 = dbhelper.timeofPackagings[4];
            ViewBag.Packaging_12 = dbhelper.timeofPackagings[5];
            ViewBag.Packaging_13 = dbhelper.timeofPackagings[6];
            ViewBag.Packaging_14 = dbhelper.timeofPackagings[7];
            ViewBag.Packaging_15 = dbhelper.timeofPackagings[8];
            ViewBag.Packaging_16 = dbhelper.timeofPackagings[9];
            ViewBag.Packaging_17 = dbhelper.timeofPackagings[10];
            ViewBag.Transport_7 = dbhelper.timeofTransports[0];
            ViewBag.Transport_8 = dbhelper.timeofTransports[1];
            ViewBag.Transport_9 = dbhelper.timeofTransports[2];
            ViewBag.Transport_10 = dbhelper.timeofTransports[3];
            ViewBag.Transport_11 = dbhelper.timeofTransports[4];
            ViewBag.Transport_12 = dbhelper.timeofTransports[5];
            ViewBag.Transport_13 = dbhelper.timeofTransports[6];
            ViewBag.Transport_14 = dbhelper.timeofTransports[7];
            ViewBag.Transport_15 = dbhelper.timeofTransports[8];
            ViewBag.Transport_16 = dbhelper.timeofTransports[9];
            ViewBag.Transport_17 = dbhelper.timeofTransports[10];
            return View();
           
        }
        public ActionResult home_third_1()
        {
            string tomorrow = DateTime.Now.AddDays(2).ToString("yyyy-MM-dd");
            DateTime date = Convert.ToDateTime(tomorrow);
            Dbhelper dbhelper = new Dbhelper();
            dbhelper.count("YWP", "Active", "Active", date);
            ViewBag.Message_Forklifts = dbhelper.sum[0];
            ViewBag.Message_Packagings = dbhelper.sum[1];
            ViewBag.Message_Transports = dbhelper.sum[2];
            ViewBag.Tomorrow = tomorrow;
            ViewBag.Forklift_7 = dbhelper.timeOfForklifts[0];
            ViewBag.Forklift_8 = dbhelper.timeOfForklifts[1];
            ViewBag.Forklift_9 = dbhelper.timeOfForklifts[2];
            ViewBag.Forklift_10 = dbhelper.timeOfForklifts[3];
            ViewBag.Forklift_11 = dbhelper.timeOfForklifts[4];
            ViewBag.Forklift_12 = dbhelper.timeOfForklifts[5];
            ViewBag.Forklift_13 = dbhelper.timeOfForklifts[6];
            ViewBag.Forklift_14 = dbhelper.timeOfForklifts[7];
            ViewBag.Forklift_15 = dbhelper.timeOfForklifts[8];
            ViewBag.Forklift_16 = dbhelper.timeOfForklifts[9];
            ViewBag.Forklift_17 = dbhelper.timeOfForklifts[10];
            ViewBag.Packaging_7 = dbhelper.timeofPackagings[0];
            ViewBag.Packaging_8 = dbhelper.timeofPackagings[1];
            ViewBag.Packaging_9 = dbhelper.timeofPackagings[2];
            ViewBag.Packaging_10 = dbhelper.timeofPackagings[3];
            ViewBag.Packaging_11 = dbhelper.timeofPackagings[4];
            ViewBag.Packaging_12 = dbhelper.timeofPackagings[5];
            ViewBag.Packaging_13 = dbhelper.timeofPackagings[6];
            ViewBag.Packaging_14 = dbhelper.timeofPackagings[7];
            ViewBag.Packaging_15 = dbhelper.timeofPackagings[8];
            ViewBag.Packaging_16 = dbhelper.timeofPackagings[9];
            ViewBag.Packaging_17 = dbhelper.timeofPackagings[10];
            ViewBag.Transport_7 = dbhelper.timeofTransports[0];
            ViewBag.Transport_8 = dbhelper.timeofTransports[1];
            ViewBag.Transport_9 = dbhelper.timeofTransports[2];
            ViewBag.Transport_10 = dbhelper.timeofTransports[3];
            ViewBag.Transport_11 = dbhelper.timeofTransports[4];
            ViewBag.Transport_12 = dbhelper.timeofTransports[5];
            ViewBag.Transport_13 = dbhelper.timeofTransports[6];
            ViewBag.Transport_14 = dbhelper.timeofTransports[7];
            ViewBag.Transport_15 = dbhelper.timeofTransports[8];
            ViewBag.Transport_16 = dbhelper.timeofTransports[9];
            ViewBag.Transport_17 = dbhelper.timeofTransports[10];
            return View();
           
        }
        public ActionResult home_today()
        {
            string tomorrow = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime date = Convert.ToDateTime(tomorrow);
            Dbhelper dbhelper = new Dbhelper();
            dbhelper.count("YSW", "Active", "Active", date);
            ViewBag.Message_Forklifts = dbhelper.sum[0];
            ViewBag.Message_Packagings = dbhelper.sum[1];
            ViewBag.Message_Transports = dbhelper.sum[2];
            ViewBag.Tomorrow = tomorrow;
            ViewBag.Forklift_7 = dbhelper.timeOfForklifts[0];
            ViewBag.Forklift_8 = dbhelper.timeOfForklifts[1];
            ViewBag.Forklift_9 = dbhelper.timeOfForklifts[2];
            ViewBag.Forklift_10 = dbhelper.timeOfForklifts[3];
            ViewBag.Forklift_11 = dbhelper.timeOfForklifts[4];
            ViewBag.Forklift_12 = dbhelper.timeOfForklifts[5];
            ViewBag.Forklift_13 = dbhelper.timeOfForklifts[6];
            ViewBag.Forklift_14 = dbhelper.timeOfForklifts[7];
            ViewBag.Forklift_15 = dbhelper.timeOfForklifts[8];
            ViewBag.Forklift_16 = dbhelper.timeOfForklifts[9];
            ViewBag.Forklift_17 = dbhelper.timeOfForklifts[10];
            ViewBag.Packaging_7 = dbhelper.timeofPackagings[0];
            ViewBag.Packaging_8 = dbhelper.timeofPackagings[1];
            ViewBag.Packaging_9 = dbhelper.timeofPackagings[2];
            ViewBag.Packaging_10 = dbhelper.timeofPackagings[3];
            ViewBag.Packaging_11 = dbhelper.timeofPackagings[4];
            ViewBag.Packaging_12 = dbhelper.timeofPackagings[5];
            ViewBag.Packaging_13 = dbhelper.timeofPackagings[6];
            ViewBag.Packaging_14 = dbhelper.timeofPackagings[7];
            ViewBag.Packaging_15 = dbhelper.timeofPackagings[8];
            ViewBag.Packaging_16 = dbhelper.timeofPackagings[9];
            ViewBag.Packaging_17 = dbhelper.timeofPackagings[10];
            ViewBag.Transport_7 = dbhelper.timeofTransports[0];
            ViewBag.Transport_8 = dbhelper.timeofTransports[1];
            ViewBag.Transport_9 = dbhelper.timeofTransports[2];
            ViewBag.Transport_10 = dbhelper.timeofTransports[3];
            ViewBag.Transport_11 = dbhelper.timeofTransports[4];
            ViewBag.Transport_12 = dbhelper.timeofTransports[5];
            ViewBag.Transport_13 = dbhelper.timeofTransports[6];
            ViewBag.Transport_14 = dbhelper.timeofTransports[7];
            ViewBag.Transport_15 = dbhelper.timeofTransports[8];
            ViewBag.Transport_16 = dbhelper.timeofTransports[9];
            ViewBag.Transport_17 = dbhelper.timeofTransports[10];
            return View();
           
        }
        public ActionResult home_today_1()
        {
            string tomorrow = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime date = Convert.ToDateTime(tomorrow);
            Dbhelper dbhelper = new Dbhelper();
            dbhelper.count("YWP", "Active", "Active", date);
            ViewBag.Message_Forklifts = dbhelper.sum[0];
            ViewBag.Message_Packagings = dbhelper.sum[1];
            ViewBag.Message_Transports = dbhelper.sum[2];
            ViewBag.Tomorrow = tomorrow;
            ViewBag.Forklift_7 = dbhelper.timeOfForklifts[0];
            ViewBag.Forklift_8 = dbhelper.timeOfForklifts[1];
            ViewBag.Forklift_9 = dbhelper.timeOfForklifts[2];
            ViewBag.Forklift_10 = dbhelper.timeOfForklifts[3];
            ViewBag.Forklift_11 = dbhelper.timeOfForklifts[4];
            ViewBag.Forklift_12 = dbhelper.timeOfForklifts[5];
            ViewBag.Forklift_13 = dbhelper.timeOfForklifts[6];
            ViewBag.Forklift_14 = dbhelper.timeOfForklifts[7];
            ViewBag.Forklift_15 = dbhelper.timeOfForklifts[8];
            ViewBag.Forklift_16 = dbhelper.timeOfForklifts[9];
            ViewBag.Forklift_17 = dbhelper.timeOfForklifts[10];
            ViewBag.Packaging_7 = dbhelper.timeofPackagings[0];
            ViewBag.Packaging_8 = dbhelper.timeofPackagings[1];
            ViewBag.Packaging_9 = dbhelper.timeofPackagings[2];
            ViewBag.Packaging_10 = dbhelper.timeofPackagings[3];
            ViewBag.Packaging_11 = dbhelper.timeofPackagings[4];
            ViewBag.Packaging_12 = dbhelper.timeofPackagings[5];
            ViewBag.Packaging_13 = dbhelper.timeofPackagings[6];
            ViewBag.Packaging_14 = dbhelper.timeofPackagings[7];
            ViewBag.Packaging_15 = dbhelper.timeofPackagings[8];
            ViewBag.Packaging_16 = dbhelper.timeofPackagings[9];
            ViewBag.Packaging_17 = dbhelper.timeofPackagings[10];
            ViewBag.Transport_7 = dbhelper.timeofTransports[0];
            ViewBag.Transport_8 = dbhelper.timeofTransports[1];
            ViewBag.Transport_9 = dbhelper.timeofTransports[2];
            ViewBag.Transport_10 = dbhelper.timeofTransports[3];
            ViewBag.Transport_11 = dbhelper.timeofTransports[4];
            ViewBag.Transport_12 = dbhelper.timeofTransports[5];
            ViewBag.Transport_13 = dbhelper.timeofTransports[6];
            ViewBag.Transport_14 = dbhelper.timeofTransports[7];
            ViewBag.Transport_15 = dbhelper.timeofTransports[8];
            ViewBag.Transport_16 = dbhelper.timeofTransports[9];
            ViewBag.Transport_17 = dbhelper.timeofTransports[10];
            return View();
           
        }
      
     
    }
}
