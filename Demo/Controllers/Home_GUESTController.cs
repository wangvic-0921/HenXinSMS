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
    public class Home_GUESTController : Controller
    {
        //
        // GET: /Home_GUEST/

        public ActionResult home()
        {
            using (var context = new HenXinSMSContext())
            {
                int[] b = new int[13];
                int[] c = new int[13];
                int[] d = new int[13];
                int[] e = new int[13];
                int[] f = new int[13];
                int[] g = new int[13];
                int[] h = new int[13];
                int[] j = new int[13];
                int[] k = new int[13];
                string t = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime datetime = Convert.ToDateTime(t);
             
                string year = DateTime.Now.ToString("yyyy");
                string month = DateTime.Now.ToString("MM");
                string day = DateTime.Now.ToString("dd");
                string tomorrow = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                DateTime date = Convert.ToDateTime(tomorrow);
                string location = "YSW";
                string status = "Active";
                string schedule = "Active";
                var forklifts_1 = from w in context.Forklifts
                                  where w.Location == location && w.Status == status
                                  select w;
                var forklifts = from w in forklifts_1
                                where w.EstimateDate_Use == date && w.Schedule == schedule
                                select w;
                int count_Forklifts = forklifts_1.Count(w => w.EstimateDate_Use == date);
                ViewBag.Message_Forklifts = count_Forklifts;
                var packagings_1 = from w in context.Packagings
                                   where w.Location == location && w.Status == status
                                   select w;
                var packagings = from w in packagings_1
                                 where w.EstimateDate_Use == date && w.Schedule == schedule
                                 select w;
                int count_Packagings = packagings_1.Count(w => w.EstimateDate_Use == date);
                ViewBag.Message_Packagings = count_Packagings;
                var transports_1 = from w in context.Transports
                                   where w.Location == location && w.Status == status
                                   select w;
                var transports = from w in transports_1
                                 where w.EstimateDate_Use == date && w.Schedule == schedule
                                 select w;
                int count_Transports = transports_1.Count(w => w.EstimateDate_Use == date);
                ViewBag.Message_Transports = count_Transports;
                foreach (var forklift in forklifts)
                {
                    int m;
                    int n;
                    int s;
                    m = forklift.EstimateTime_Start.Hour;
                    n = forklift.EstimateTime_End.Hour;
                    s = forklift.EstimateTime_End.Minute;
                    if (s != 0)
                    {
                        n = n + 1;
                    }

                    apply test = new apply(m, n);
                    b = test.show();
                    for (int i = 0; i < 13; i++)
                    {
                        c[i] = c[i] + b[i];

                    }
                }
                foreach (var packaging in packagings)
                {
                    int m;
                    int n;
                    int s;
                    m = packaging.EstimateTime_Start.Hour;
                    n = packaging.EstimateTime_End.Hour;
                    s = packaging.EstimateTime_End.Minute;
                    if (s != 0)
                    {
                        n = n + 1;
                    }
                    apply test = new apply(m, n);
                    e = test.show();
                    for (int i = 0; i < 13; i++)
                    {
                        f[i] = f[i] + e[i];

                    }
                }
                foreach (var transport in transports)
                {
                    int m;
                    int n;
                    int s;
                    m = transport.EstimateTime_Start.Hour;
                    n = transport.EstimateTime_End.Hour;
                    s = transport.EstimateTime_End.Minute;
                    if (s != 0)
                    {
                        n = n + 1;
                    }
                    apply test = new apply(m, n);
                    h = test.show();
                    for (int i = 0; i < 13; i++)
                    {
                        j[i] = j[i] + h[i];

                    }

                }
                for (int i = 0; i < 13; i++)
                {
                    d[i] = d[i] + c[i];

                }
                for (int i = 0; i < 13; i++)
                {
                    g[i] = g[i] + f[i];

                }
                for (int i = 0; i < 13; i++)
                {
                    k[i] = k[i] + j[i];

                }

                ViewBag.n = d[7];
                ViewBag.Forklift_7 = d[0];
                ViewBag.Forklift_8 = d[1];
                ViewBag.Forklift_9 = d[2];
                ViewBag.Forklift_10 = d[3];
                ViewBag.Forklift_11 = d[4];
                ViewBag.Forklift_12 = d[5];
                ViewBag.Forklift_13 = d[6];
                ViewBag.Forklift_14 = d[7];
                ViewBag.Forklift_15 = d[8];
                ViewBag.Forklift_16 = d[9];
                ViewBag.Forklift_17 = d[10];
                ViewBag.Packaging_7 = g[0];
                ViewBag.Packaging_8 = g[1];
                ViewBag.Packaging_9 = g[2];
                ViewBag.Packaging_10 = g[3];
                ViewBag.Packaging_11 = g[4];
                ViewBag.Packaging_12 = g[5];
                ViewBag.Packaging_13 = g[6];
                ViewBag.Packaging_14 = g[7];
                ViewBag.Packaging_15 = g[8];
                ViewBag.Packaging_16 = g[9];
                ViewBag.Packaging_17 = g[10];
                ViewBag.Transport_7 = k[0];
                ViewBag.Transport_8 = k[1];
                ViewBag.Transport_9 = k[2];
                ViewBag.Transport_10 = k[3];
                ViewBag.Transport_11 = k[4];
                ViewBag.Transport_12 = k[5];
                ViewBag.Transport_13 = k[6];
                ViewBag.Transport_14 = k[7];
                ViewBag.Transport_15 = k[8];
                ViewBag.Transport_16 = k[9];
                ViewBag.Transport_17 = k[10];
                Array.Clear(c, 0, c.Length);
                Array.Clear(f, 0, f.Length);
                Array.Clear(j, 0, j.Length);
                ViewBag.Tomorrow = tomorrow;
                ViewBag.NowYear = DateTime.Now.ToString("yyyy");
                ViewBag.NowMouth = DateTime.Now.ToString("MM");
                ViewBag.NowDay = DateTime.Now.ToString("dd");
                return View();
            }
        }
        public ActionResult home_1()
        {
            using (var context = new HenXinSMSContext())
            {
                int[] b = new int[13];
                int[] c = new int[13];
                int[] d = new int[13];
                int[] e = new int[13];
                int[] f = new int[13];
                int[] g = new int[13];
                int[] h = new int[13];
                int[] j = new int[13];
                int[] k = new int[13];
                string t = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime datetime = Convert.ToDateTime(t);
              
                string year = DateTime.Now.ToString("yyyy");
                string month = DateTime.Now.ToString("MM");
                string day = DateTime.Now.ToString("dd");
                string tomorrow = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                DateTime date = Convert.ToDateTime(tomorrow);
                string location = "YWP";
                string status = "Active";
                string schedule = "Active";
                var forklifts_1 = from w in context.Forklifts
                                  where w.Location == location && w.Status == status
                                  select w;
                var forklifts = from w in forklifts_1
                                where w.EstimateDate_Use == date && w.Schedule == schedule
                                select w;
                int count_Forklifts = forklifts_1.Count(w => w.EstimateDate_Use == date);
                ViewBag.Message_Forklifts = count_Forklifts;
                var packagings_1 = from w in context.Packagings
                                   where w.Location == location && w.Status == status
                                   select w;
                var packagings = from w in packagings_1
                                 where w.EstimateDate_Use == date && w.Schedule == schedule
                                 select w;
                int count_Packagings = packagings_1.Count(w => w.EstimateDate_Use == date);
                ViewBag.Message_Packagings = count_Packagings;
                var transports_1 = from w in context.Transports
                                   where w.Location == location && w.Status == status
                                   select w;
                var transports = from w in transports_1
                                 where w.EstimateDate_Use == date && w.Schedule == schedule
                                 select w;
                int count_Transports = transports_1.Count(w => w.EstimateDate_Use == date);
                ViewBag.Message_Transports = count_Transports;
                foreach (var forklift in forklifts)
                {
                    int m;
                    int n;
                    int s;
                    m = forklift.EstimateTime_Start.Hour;
                    n = forklift.EstimateTime_End.Hour;
                    s = forklift.EstimateTime_End.Minute;
                    if (s != 0)
                    {
                        n = n + 1;
                    }

                    apply test = new apply(m, n);
                    b = test.show();
                    for (int i = 0; i < 13; i++)
                    {
                        c[i] = c[i] + b[i];

                    }
                }
                foreach (var packaging in packagings)
                {
                    int m;
                    int n;
                    int s;
                    m = packaging.EstimateTime_Start.Hour;
                    n = packaging.EstimateTime_End.Hour;
                    s = packaging.EstimateTime_End.Minute;
                    if (s != 0)
                    {
                        n = n + 1;
                    }
                    apply test = new apply(m, n);
                    e = test.show();
                    for (int i = 0; i < 13; i++)
                    {
                        f[i] = f[i] + e[i];

                    }
                }
                foreach (var transport in transports)
                {
                    int m;
                    int n;
                    int s;
                    m = transport.EstimateTime_Start.Hour;
                    n = transport.EstimateTime_End.Hour;
                    s = transport.EstimateTime_End.Minute;
                    if (s != 0)
                    {
                        n = n + 1;
                    }
                    apply test = new apply(m, n);
                    h = test.show();
                    for (int i = 0; i < 13; i++)
                    {
                        j[i] = j[i] + h[i];

                    }

                }
                for (int i = 0; i < 13; i++)
                {
                    d[i] = d[i] + c[i];

                }
                for (int i = 0; i < 13; i++)
                {
                    g[i] = g[i] + f[i];

                }
                for (int i = 0; i < 13; i++)
                {
                    k[i] = k[i] + j[i];

                }

                ViewBag.n = d[7];
                ViewBag.Forklift_7 = d[0];
                ViewBag.Forklift_8 = d[1];
                ViewBag.Forklift_9 = d[2];
                ViewBag.Forklift_10 = d[3];
                ViewBag.Forklift_11 = d[4];
                ViewBag.Forklift_12 = d[5];
                ViewBag.Forklift_13 = d[6];
                ViewBag.Forklift_14 = d[7];
                ViewBag.Forklift_15 = d[8];
                ViewBag.Forklift_16 = d[9];
                ViewBag.Forklift_17 = d[10];
                ViewBag.Packaging_7 = g[0];
                ViewBag.Packaging_8 = g[1];
                ViewBag.Packaging_9 = g[2];
                ViewBag.Packaging_10 = g[3];
                ViewBag.Packaging_11 = g[4];
                ViewBag.Packaging_12 = g[5];
                ViewBag.Packaging_13 = g[6];
                ViewBag.Packaging_14 = g[7];
                ViewBag.Packaging_15 = g[8];
                ViewBag.Packaging_16 = g[9];
                ViewBag.Packaging_17 = g[10];
                ViewBag.Transport_7 = k[0];
                ViewBag.Transport_8 = k[1];
                ViewBag.Transport_9 = k[2];
                ViewBag.Transport_10 = k[3];
                ViewBag.Transport_11 = k[4];
                ViewBag.Transport_12 = k[5];
                ViewBag.Transport_13 = k[6];
                ViewBag.Transport_14 = k[7];
                ViewBag.Transport_15 = k[8];
                ViewBag.Transport_16 = k[9];
                ViewBag.Transport_17 = k[10];
                Array.Clear(c, 0, c.Length);
                Array.Clear(f, 0, f.Length);
                Array.Clear(j, 0, j.Length);
                ViewBag.Tomorrow = tomorrow;
                ViewBag.NowYear = DateTime.Now.ToString("yyyy");
                ViewBag.NowMouth = DateTime.Now.ToString("MM");
                ViewBag.NowDay = DateTime.Now.ToString("dd");
                return View();
            }
        }
        public ActionResult home_third()
        {
            using (var context = new HenXinSMSContext())
            {
                int[] b = new int[13];
                int[] c = new int[13];
                int[] d = new int[13];
                int[] e = new int[13];
                int[] f = new int[13];
                int[] g = new int[13];
                int[] h = new int[13];
                int[] j = new int[13];
                int[] k = new int[13];
                string t = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime datetime = Convert.ToDateTime(t);
            
                string year = DateTime.Now.ToString("yyyy");
                string month = DateTime.Now.ToString("MM");
                string day = DateTime.Now.ToString("dd");
                string tomorrow = DateTime.Now.AddDays(2).ToString("yyyy-MM-dd");
                DateTime date = Convert.ToDateTime(tomorrow);
                string location = "YSW";
                string status = "Active";
                string schedule = "Active";
                var forklifts_1 = from w in context.Forklifts
                                  where w.Location == location && w.Status == status
                                  select w;
                var forklifts = from w in forklifts_1
                                where w.EstimateDate_Use == date && w.Schedule == schedule
                                select w;
                int count_Forklifts = forklifts_1.Count(w => w.EstimateDate_Use == date);
                ViewBag.Message_Forklifts = count_Forklifts;
                var packagings_1 = from w in context.Packagings
                                   where w.Location == location && w.Status == status
                                   select w;
                var packagings = from w in packagings_1
                                 where w.EstimateDate_Use == date && w.Schedule == schedule
                                 select w;
                int count_Packagings = packagings_1.Count(w => w.EstimateDate_Use == date);
                ViewBag.Message_Packagings = count_Packagings;
                var transports_1 = from w in context.Transports
                                   where w.Location == location && w.Status == status
                                   select w;
                var transports = from w in transports_1
                                 where w.EstimateDate_Use == date && w.Schedule == schedule
                                 select w;
                int count_Transports = transports_1.Count(w => w.EstimateDate_Use == date);
                ViewBag.Message_Transports = count_Transports;
                foreach (var forklift in forklifts)
                {
                    int m;
                    int n;
                    int s;
                    m = forklift.EstimateTime_Start.Hour;
                    n = forklift.EstimateTime_End.Hour;
                    s = forklift.EstimateTime_End.Minute;
                    if (s != 0)
                    {
                        n = n + 1;
                    }

                    apply test = new apply(m, n);
                    b = test.show();
                    for (int i = 0; i < 13; i++)
                    {
                        c[i] = c[i] + b[i];

                    }
                }
                foreach (var packaging in packagings)
                {
                    int m;
                    int n;
                    int s;
                    m = packaging.EstimateTime_Start.Hour;
                    n = packaging.EstimateTime_End.Hour;
                    s = packaging.EstimateTime_End.Minute;
                    if (s != 0)
                    {
                        n = n + 1;
                    }
                    apply test = new apply(m, n);
                    e = test.show();
                    for (int i = 0; i < 13; i++)
                    {
                        f[i] = f[i] + e[i];

                    }
                }
                foreach (var transport in transports)
                {
                    int m;
                    int n;
                    int s;
                    m = transport.EstimateTime_Start.Hour;
                    n = transport.EstimateTime_End.Hour;
                    s = transport.EstimateTime_End.Minute;
                    if (s != 0)
                    {
                        n = n + 1;
                    }
                    apply test = new apply(m, n);
                    h = test.show();
                    for (int i = 0; i < 13; i++)
                    {
                        j[i] = j[i] + h[i];

                    }

                }
                for (int i = 0; i < 13; i++)
                {
                    d[i] = d[i] + c[i];

                }
                for (int i = 0; i < 13; i++)
                {
                    g[i] = g[i] + f[i];

                }
                for (int i = 0; i < 13; i++)
                {
                    k[i] = k[i] + j[i];

                }
                Array.Clear(c, 0, c.Length);
                Array.Clear(f, 0, f.Length);
                Array.Clear(j, 0, j.Length);
                ViewBag.n = d[7];
                ViewBag.Forklift_7 = d[0];
                ViewBag.Forklift_8 = d[1];
                ViewBag.Forklift_9 = d[2];
                ViewBag.Forklift_10 = d[3];
                ViewBag.Forklift_11 = d[4];
                ViewBag.Forklift_12 = d[5];
                ViewBag.Forklift_13 = d[6];
                ViewBag.Forklift_14 = d[7];
                ViewBag.Forklift_15 = d[8];
                ViewBag.Forklift_16 = d[9];
                ViewBag.Forklift_17 = d[10];
                ViewBag.Packaging_7 = g[0];
                ViewBag.Packaging_8 = g[1];
                ViewBag.Packaging_9 = g[2];
                ViewBag.Packaging_10 = g[3];
                ViewBag.Packaging_11 = g[4];
                ViewBag.Packaging_12 = g[5];
                ViewBag.Packaging_13 = g[6];
                ViewBag.Packaging_14 = g[7];
                ViewBag.Packaging_15 = g[8];
                ViewBag.Packaging_16 = g[9];
                ViewBag.Packaging_17 = g[10];
                ViewBag.Transport_7 = k[0];
                ViewBag.Transport_8 = k[1];
                ViewBag.Transport_9 = k[2];
                ViewBag.Transport_10 = k[3];
                ViewBag.Transport_11 = k[4];
                ViewBag.Transport_12 = k[5];
                ViewBag.Transport_13 = k[6];
                ViewBag.Transport_14 = k[7];
                ViewBag.Transport_15 = k[8];
                ViewBag.Transport_16 = k[9];
                ViewBag.Transport_17 = k[10];
                ViewBag.Tomorrow = tomorrow;
                ViewBag.NowYear = DateTime.Now.ToString("yyyy");
                ViewBag.NowMouth = DateTime.Now.ToString("MM");
                ViewBag.NowDay = DateTime.Now.ToString("dd");
                return View();
            }
        }
        public ActionResult home_third_1()
        {
            using (var context = new HenXinSMSContext())
            {
                int[] b = new int[13];
                int[] c = new int[13];
                int[] d = new int[13];
                int[] e = new int[13];
                int[] f = new int[13];
                int[] g = new int[13];
                int[] h = new int[13];
                int[] j = new int[13];
                int[] k = new int[13];
                string t = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime datetime = Convert.ToDateTime(t);
             
                string year = DateTime.Now.ToString("yyyy");
                string month = DateTime.Now.ToString("MM");
                string day = DateTime.Now.ToString("dd");
                string tomorrow = DateTime.Now.AddDays(2).ToString("yyyy-MM-dd");
                DateTime date = Convert.ToDateTime(tomorrow);
                string location = "YWP";
                string status = "Active";
                string schedule = "Active";
                var forklifts_1 = from w in context.Forklifts
                                  where w.Location == location && w.Status == status
                                  select w;
                var forklifts = from w in forklifts_1
                                where w.EstimateDate_Use == date && w.Schedule == schedule
                                select w;
                int count_Forklifts = forklifts_1.Count(w => w.EstimateDate_Use == date);
                ViewBag.Message_Forklifts = count_Forklifts;
                var packagings_1 = from w in context.Packagings
                                   where w.Location == location && w.Status == status
                                   select w;
                var packagings = from w in packagings_1
                                 where w.EstimateDate_Use == date && w.Schedule == schedule
                                 select w;
                int count_Packagings = packagings_1.Count(w => w.EstimateDate_Use == date);
                ViewBag.Message_Packagings = count_Packagings;
                var transports_1 = from w in context.Transports
                                   where w.Location == location && w.Status == status
                                   select w;
                var transports = from w in transports_1
                                 where w.EstimateDate_Use == date && w.Schedule == schedule
                                 select w;
                int count_Transports = transports_1.Count(w => w.EstimateDate_Use == date);
                ViewBag.Message_Transports = count_Transports;
                foreach (var forklift in forklifts)
                {
                    int m;
                    int n;
                    int s;
                    m = forklift.EstimateTime_Start.Hour;
                    n = forklift.EstimateTime_End.Hour;
                    s = forklift.EstimateTime_End.Minute;
                    if (s != 0)
                    {
                        n = n + 1;
                    }

                    apply test = new apply(m, n);
                    b = test.show();
                    for (int i = 0; i < 13; i++)
                    {
                        c[i] = c[i] + b[i];

                    }
                }
                foreach (var packaging in packagings)
                {
                    int m;
                    int n;
                    int s;
                    m = packaging.EstimateTime_Start.Hour;
                    n = packaging.EstimateTime_End.Hour;
                    s = packaging.EstimateTime_End.Minute;
                    if (s != 0)
                    {
                        n = n + 1;
                    }
                    apply test = new apply(m, n);
                    e = test.show();
                    for (int i = 0; i < 13; i++)
                    {
                        f[i] = f[i] + e[i];

                    }
                }
                foreach (var transport in transports)
                {
                    int m;
                    int n;
                    int s;
                    m = transport.EstimateTime_Start.Hour;
                    n = transport.EstimateTime_End.Hour;
                    s = transport.EstimateTime_End.Minute;
                    if (s != 0)
                    {
                        n = n + 1;
                    }
                    apply test = new apply(m, n);
                    h = test.show();
                    for (int i = 0; i < 13; i++)
                    {
                        j[i] = j[i] + h[i];

                    }

                }
                for (int i = 0; i < 13; i++)
                {
                    d[i] = d[i] + c[i];

                }
                for (int i = 0; i < 13; i++)
                {
                    g[i] = g[i] + f[i];

                }
                for (int i = 0; i < 13; i++)
                {
                    k[i] = k[i] + j[i];

                }
                Array.Clear(c, 0, c.Length);
                Array.Clear(f, 0, f.Length);
                Array.Clear(j, 0, j.Length);
                ViewBag.n = d[7];
                ViewBag.Forklift_7 = d[0];
                ViewBag.Forklift_8 = d[1];
                ViewBag.Forklift_9 = d[2];
                ViewBag.Forklift_10 = d[3];
                ViewBag.Forklift_11 = d[4];
                ViewBag.Forklift_12 = d[5];
                ViewBag.Forklift_13 = d[6];
                ViewBag.Forklift_14 = d[7];
                ViewBag.Forklift_15 = d[8];
                ViewBag.Forklift_16 = d[9];
                ViewBag.Forklift_17 = d[10];
                ViewBag.Packaging_7 = g[0];
                ViewBag.Packaging_8 = g[1];
                ViewBag.Packaging_9 = g[2];
                ViewBag.Packaging_10 = g[3];
                ViewBag.Packaging_11 = g[4];
                ViewBag.Packaging_12 = g[5];
                ViewBag.Packaging_13 = g[6];
                ViewBag.Packaging_14 = g[7];
                ViewBag.Packaging_15 = g[8];
                ViewBag.Packaging_16 = g[9];
                ViewBag.Packaging_17 = g[10];
                ViewBag.Transport_7 = k[0];
                ViewBag.Transport_8 = k[1];
                ViewBag.Transport_9 = k[2];
                ViewBag.Transport_10 = k[3];
                ViewBag.Transport_11 = k[4];
                ViewBag.Transport_12 = k[5];
                ViewBag.Transport_13 = k[6];
                ViewBag.Transport_14 = k[7];
                ViewBag.Transport_15 = k[8];
                ViewBag.Transport_16 = k[9];
                ViewBag.Transport_17 = k[10];
                ViewBag.Tomorrow = tomorrow;
                ViewBag.NowYear = DateTime.Now.ToString("yyyy");
                ViewBag.NowMouth = DateTime.Now.ToString("MM");
                ViewBag.NowDay = DateTime.Now.ToString("dd");
                return View();
            }
        }
        public ActionResult home_today()
        {
            using (var context = new HenXinSMSContext())
            {
                int[] b = new int[13];
                int[] c = new int[13];
                int[] d = new int[13];
                int[] e = new int[13];
                int[] f = new int[13];
                int[] g = new int[13];
                int[] h = new int[13];
                int[] j = new int[13];
                int[] k = new int[13];
                string t = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime datetime = Convert.ToDateTime(t);
             
                string year = DateTime.Now.ToString("yyyy");
                string month = DateTime.Now.ToString("MM");
                string day = DateTime.Now.ToString("dd");
                string tomorrow = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime date = Convert.ToDateTime(tomorrow);
                string location = "YSW";
                string status = "Active";
                string schedule = "Active";
                var forklifts_1 = from w in context.Forklifts
                                  where w.Location == location && w.Status == status
                                  select w;
                var forklifts = from w in forklifts_1
                                where w.EstimateDate_Use == date && w.Schedule == schedule
                                select w;
                int count_Forklifts = forklifts_1.Count(w => w.EstimateDate_Use == date);
                ViewBag.Message_Forklifts = count_Forklifts;
                var packagings_1 = from w in context.Packagings
                                   where w.Location == location && w.Status == status
                                   select w;
                var packagings = from w in packagings_1
                                 where w.EstimateDate_Use == date && w.Schedule == schedule
                                 select w;
                int count_Packagings = packagings_1.Count(w => w.EstimateDate_Use == date);
                ViewBag.Message_Packagings = count_Packagings;
                var transports_1 = from w in context.Transports
                                   where w.Location == location && w.Status == status
                                   select w;
                var transports = from w in transports_1
                                 where w.EstimateDate_Use == date && w.Schedule == schedule
                                 select w;
                int count_Transports = transports_1.Count(w => w.EstimateDate_Use == date);
                ViewBag.Message_Transports = count_Transports;
                foreach (var forklift in forklifts)
                {
                    int m;
                    int n;
                    int s;
                    m = forklift.EstimateTime_Start.Hour;
                    n = forklift.EstimateTime_End.Hour;
                    s = forklift.EstimateTime_End.Minute;
                    if (s != 0)
                    {
                        n = n + 1;
                    }

                    apply test = new apply(m, n);
                    b = test.show();
                    for (int i = 0; i < 13; i++)
                    {
                        c[i] = c[i] + b[i];

                    }
                }
                foreach (var packaging in packagings)
                {
                    int m;
                    int n;
                    int s;
                    m = packaging.EstimateTime_Start.Hour;
                    n = packaging.EstimateTime_End.Hour;
                    s = packaging.EstimateTime_End.Minute;
                    if (s != 0)
                    {
                        n = n + 1;
                    }
                    apply test = new apply(m, n);
                    e = test.show();
                    for (int i = 0; i < 13; i++)
                    {
                        f[i] = f[i] + e[i];

                    }
                }
                foreach (var transport in transports)
                {
                    int m;
                    int n;
                    int s;
                    m = transport.EstimateTime_Start.Hour;
                    n = transport.EstimateTime_End.Hour;
                    s = transport.EstimateTime_End.Minute;
                    if (s != 0)
                    {
                        n = n + 1;
                    }
                    apply test = new apply(m, n);
                    h = test.show();
                    for (int i = 0; i < 13; i++)
                    {
                        j[i] = j[i] + h[i];

                    }

                }
                for (int i = 0; i < 13; i++)
                {
                    d[i] = d[i] + c[i];

                }
                for (int i = 0; i < 13; i++)
                {
                    g[i] = g[i] + f[i];

                }
                for (int i = 0; i < 13; i++)
                {
                    k[i] = k[i] + j[i];

                }
                Array.Clear(c, 0, c.Length);
                Array.Clear(f, 0, f.Length);
                Array.Clear(j, 0, j.Length);
                ViewBag.n = d[7];
                ViewBag.Forklift_7 = d[0];
                ViewBag.Forklift_8 = d[1];
                ViewBag.Forklift_9 = d[2];
                ViewBag.Forklift_10 = d[3];
                ViewBag.Forklift_11 = d[4];
                ViewBag.Forklift_12 = d[5];
                ViewBag.Forklift_13 = d[6];
                ViewBag.Forklift_14 = d[7];
                ViewBag.Forklift_15 = d[8];
                ViewBag.Forklift_16 = d[9];
                ViewBag.Forklift_17 = d[10];
                ViewBag.Packaging_7 = g[0];
                ViewBag.Packaging_8 = g[1];
                ViewBag.Packaging_9 = g[2];
                ViewBag.Packaging_10 = g[3];
                ViewBag.Packaging_11 = g[4];
                ViewBag.Packaging_12 = g[5];
                ViewBag.Packaging_13 = g[6];
                ViewBag.Packaging_14 = g[7];
                ViewBag.Packaging_15 = g[8];
                ViewBag.Packaging_16 = g[9];
                ViewBag.Packaging_17 = g[10];
                ViewBag.Transport_7 = k[0];
                ViewBag.Transport_8 = k[1];
                ViewBag.Transport_9 = k[2];
                ViewBag.Transport_10 = k[3];
                ViewBag.Transport_11 = k[4];
                ViewBag.Transport_12 = k[5];
                ViewBag.Transport_13 = k[6];
                ViewBag.Transport_14 = k[7];
                ViewBag.Transport_15 = k[8];
                ViewBag.Transport_16 = k[9];
                ViewBag.Transport_17 = k[10];
                ViewBag.Tomorrow = tomorrow;
                ViewBag.NowYear = DateTime.Now.ToString("yyyy");
                ViewBag.NowMouth = DateTime.Now.ToString("MM");
                ViewBag.NowDay = DateTime.Now.ToString("dd");
                return View();
            }
        }
        public ActionResult home_today_1()
        {
            using (var context = new HenXinSMSContext())
            {
                int[] b = new int[13];
                int[] c = new int[13];
                int[] d = new int[13];
                int[] e = new int[13];
                int[] f = new int[13];
                int[] g = new int[13];
                int[] h = new int[13];
                int[] j = new int[13];
                int[] k = new int[13];
                string t = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime datetime = Convert.ToDateTime(t);
              
                string year = DateTime.Now.ToString("yyyy");
                string month = DateTime.Now.ToString("MM");
                string day = DateTime.Now.ToString("dd");
                string tomorrow = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime date = Convert.ToDateTime(tomorrow);
                string location = "YWP";
                string status = "Active";
                string schedule = "Active";
                var forklifts_1 = from w in context.Forklifts
                                  where w.Location == location && w.Status == status
                                  select w;
                var forklifts = from w in forklifts_1
                                where w.EstimateDate_Use == date && w.Schedule == schedule
                                select w;
                int count_Forklifts = forklifts_1.Count(w => w.EstimateDate_Use == date);
                ViewBag.Message_Forklifts = count_Forklifts;
                var packagings_1 = from w in context.Packagings
                                   where w.Location == location && w.Status == status
                                   select w;
                var packagings = from w in packagings_1
                                 where w.EstimateDate_Use == date && w.Schedule == schedule
                                 select w;
                int count_Packagings = packagings_1.Count(w => w.EstimateDate_Use == date);
                ViewBag.Message_Packagings = count_Packagings;
                var transports_1 = from w in context.Transports
                                   where w.Location == location && w.Status == status
                                   select w;
                var transports = from w in transports_1
                                 where w.EstimateDate_Use == date && w.Schedule == schedule
                                 select w;
                int count_Transports = transports_1.Count(w => w.EstimateDate_Use == date);
                ViewBag.Message_Transports = count_Transports;
                foreach (var forklift in forklifts)
                {
                    int m;
                    int n;
                    int s;
                    m = forklift.EstimateTime_Start.Hour;
                    n = forklift.EstimateTime_End.Hour;
                    s = forklift.EstimateTime_End.Minute;
                    if (s != 0)
                    {
                        n = n + 1;
                    }

                    apply test = new apply(m, n);
                    b = test.show();
                    for (int i = 0; i < 13; i++)
                    {
                        c[i] = c[i] + b[i];

                    }
                }
                foreach (var packaging in packagings)
                {
                    int m;
                    int n;
                    int s;
                    m = packaging.EstimateTime_Start.Hour;
                    n = packaging.EstimateTime_End.Hour;
                    s = packaging.EstimateTime_End.Minute;
                    if (s != 0)
                    {
                        n = n + 1;
                    }
                    apply test = new apply(m, n);
                    e = test.show();
                    for (int i = 0; i < 13; i++)
                    {
                        f[i] = f[i] + e[i];

                    }
                }
                foreach (var transport in transports)
                {
                    int m;
                    int n;
                    int s;
                    m = transport.EstimateTime_Start.Hour;
                    n = transport.EstimateTime_End.Hour;
                    s = transport.EstimateTime_End.Minute;
                    if (s != 0)
                    {
                        n = n + 1;
                    }
                    apply test = new apply(m, n);
                    h = test.show();
                    for (int i = 0; i < 13; i++)
                    {
                        j[i] = j[i] + h[i];

                    }

                }
                for (int i = 0; i < 13; i++)
                {
                    d[i] = d[i] + c[i];

                }
                for (int i = 0; i < 13; i++)
                {
                    g[i] = g[i] + f[i];

                }
                for (int i = 0; i < 13; i++)
                {
                    k[i] = k[i] + j[i];

                }
                Array.Clear(c, 0, c.Length);
                Array.Clear(f, 0, f.Length);
                Array.Clear(j, 0, j.Length);
                ViewBag.n = d[7];
                ViewBag.Forklift_7 = d[0];
                ViewBag.Forklift_8 = d[1];
                ViewBag.Forklift_9 = d[2];
                ViewBag.Forklift_10 = d[3];
                ViewBag.Forklift_11 = d[4];
                ViewBag.Forklift_12 = d[5];
                ViewBag.Forklift_13 = d[6];
                ViewBag.Forklift_14 = d[7];
                ViewBag.Forklift_15 = d[8];
                ViewBag.Forklift_16 = d[9];
                ViewBag.Forklift_17 = d[10];
                ViewBag.Packaging_7 = g[0];
                ViewBag.Packaging_8 = g[1];
                ViewBag.Packaging_9 = g[2];
                ViewBag.Packaging_10 = g[3];
                ViewBag.Packaging_11 = g[4];
                ViewBag.Packaging_12 = g[5];
                ViewBag.Packaging_13 = g[6];
                ViewBag.Packaging_14 = g[7];
                ViewBag.Packaging_15 = g[8];
                ViewBag.Packaging_16 = g[9];
                ViewBag.Packaging_17 = g[10];
                ViewBag.Transport_7 = k[0];
                ViewBag.Transport_8 = k[1];
                ViewBag.Transport_9 = k[2];
                ViewBag.Transport_10 = k[3];
                ViewBag.Transport_11 = k[4];
                ViewBag.Transport_12 = k[5];
                ViewBag.Transport_13 = k[6];
                ViewBag.Transport_14 = k[7];
                ViewBag.Transport_15 = k[8];
                ViewBag.Transport_16 = k[9];
                ViewBag.Transport_17 = k[10];
                ViewBag.Tomorrow = tomorrow;
                ViewBag.NowYear = DateTime.Now.ToString("yyyy");
                ViewBag.NowMouth = DateTime.Now.ToString("MM");
                ViewBag.NowDay = DateTime.Now.ToString("dd");
                return View();
            }
        }
      
     
    }
}
