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
    [MemberCheck]
    public class SiteManagementController : Controller
    {
        public class viewModel
        {
            public List<Forklift> forklifts { get; set; }
            public List<Packaging> packagings { get; set; }
            public List<Transport> transports { get; set; }
            public viewModel(List<Forklift> _forklifts,List<Packaging> _packagings, List<Transport> _transports)
            {
                this.forklifts = _forklifts;
                this.packagings = _packagings;
                this.transports = _transports;
            }
        }
        public ActionResult sitemanagement()
        {
            string t = DateTime.Now.ToString("yyyy-MM");
            ViewBag.Tomorrow = t;
            using (var context = new HenXinSMSContext())
            {

                string _year = this.TempData["Year"] as string;
                string _month = this.TempData["Month"] as string;
                var vm = new viewModel(context.Forklifts.ToList(),context.Packagings.ToList(),context.Transports.ToList());
                GetDate date = new GetDate();
                DateTime firstday = date.Display_first();
                DateTime lastday = date.Display_last();
                var _forklifts = from w in context.Forklifts
                                 where w.Status == "Active" 
                                 && w.Location=="YSW"
                                 && w.EstimateDate_Use.CompareTo(firstday)>=0
                                 &&w.EstimateDate_Use.CompareTo(lastday)<=0
                                 && w.PM_Review == "已审核" 
                                 && w.OML_Review == "已审核"
                                 && w.Schedule == "Inactive"
                                 select new
                                 {
                                     ID = w.ID,
                                     ApplyId= w.ApplyId,
                                     UserName = w.UserName,
                                     Depart = w.Depart,
                                     ProjectName = w.ProjectName,
                                     ProjectNameId = w.ProjectNameId,
                                     EstimateDate_Use = w.EstimateDate_Use,
                                     EstimateTime_Start = w.EstimateTime_Start,
                                     EstimateTime_End = w.EstimateTime_End,
                                     PM_Review = w.PM_Review,
                                     OML_Review = w.OML_Review,
                                     Flag=w.Flag,
                                     Schedule= w.Schedule
                                 };
                var _packagings = from w in context.Packagings
                                  where w.Status == "Active"
                                        && w.Location == "YSW"
                                        && w.EstimateDate_Use.CompareTo(firstday) >= 0 
                                        && w.EstimateDate_Use.CompareTo(lastday) <= 0 
                                        && w.PM_Review == "已审核" 
                                        && w.OML_Review == "已审核"
                                        && w.Schedule == "Inactive"
                                 select new
                                 {
                                     ID = w.ID,
                                     ApplyId = w.ApplyId,
                                     UserName = w.UserName,
                                     Depart = w.Depart,
                                     ProjectName = w.ProjectName,
                                     ProjectNameId = w.ProjectNameId,
                                     EstimateDate_Use = w.EstimateDate_Use,
                                     EstimateTime_Start = w.EstimateTime_Start,
                                     EstimateTime_End = w.EstimateTime_End,
                                     PM_Review = w.PM_Review,
                                     OML_Review = w.OML_Review,
                                     Flag = w.Flag,
                                     Schedule = w.Schedule
                                 };
                var _transports = from w in context.Transports
                                  where w.Status == "Active"
                                                && w.Location == "YSW"
                                                && w.EstimateDate_Use.CompareTo(firstday) >= 0
                                                && w.EstimateDate_Use.CompareTo(lastday) <= 0
                                                && w.PM_Review == "已审核" 
                                                && w.OML_Review == "已审核"
                                                && w.Schedule == "Inactive"
                                 select new
                                 {
                                     ID = w.ID,
                                     ApplyId = w.ApplyId,
                                     UserName = w.UserName,
                                     Depart = w.Depart,
                                     ProjectName = w.ProjectName,
                                     ProjectNameId = w.ProjectNameId,
                                     EstimateDate_Use = w.EstimateDate_Use,
                                     EstimateTime_Start = w.EstimateTime_Start,
                                     EstimateTime_End = w.EstimateTime_End,
                                     PM_Review = w.PM_Review,
                                     OML_Review = w.OML_Review,
                                     Flag = w.Flag,
                                     Schedule = w.Schedule
                                 };
                vm.forklifts = _forklifts.ToList().Select(w => new Forklift
                {
                    ID = w.ID,
                    ApplyId = w.ApplyId,
                    UserName = w.UserName,
                    Depart = w.Depart,
                    ProjectName = w.ProjectName,
                    ProjectNameId = w.ProjectNameId,
                    EstimateDate_Use = w.EstimateDate_Use,
                    EstimateTime_Start = w.EstimateTime_Start,
                    EstimateTime_End = w.EstimateTime_End,
                    PM_Review = w.PM_Review,
                    OML_Review = w.OML_Review,
                    Flag = w.Flag,
                    Schedule = w.Schedule
                }).ToList();
                vm.packagings = _packagings.ToList().Select(w => new Packaging
                {
                    ID = w.ID,
                    ApplyId = w.ApplyId,
                    UserName = w.UserName,
                    Depart = w.Depart,
                    ProjectName = w.ProjectName,
                    ProjectNameId = w.ProjectNameId,
                    EstimateDate_Use = w.EstimateDate_Use,
                    EstimateTime_Start = w.EstimateTime_Start,
                    EstimateTime_End = w.EstimateTime_End,
                    PM_Review = w.PM_Review,
                    OML_Review = w.OML_Review,
                    Flag = w.Flag,
                    Schedule = w.Schedule
                }).ToList();
                vm.transports = _transports.ToList().Select(w => new Transport
                {
                    ID = w.ID,
                    ApplyId = w.ApplyId,
                    UserName = w.UserName,
                    Depart = w.Depart,
                    ProjectName = w.ProjectName,
                    ProjectNameId = w.ProjectNameId,
                    EstimateDate_Use = w.EstimateDate_Use,
                    EstimateTime_Start = w.EstimateTime_Start,
                    EstimateTime_End = w.EstimateTime_End,
                    PM_Review = w.PM_Review,
                    OML_Review = w.OML_Review,
                    Flag = w.Flag,
                    Schedule = w.Schedule
                }).ToList();
                return View(vm);
            }
        }
        public ActionResult sitemanagement_completed()
        {
            string t = DateTime.Now.ToString("yyyy-MM");
            ViewBag.Tomorrow = t;
            using (var context = new HenXinSMSContext())
            {

                string _year = this.TempData["Year"] as string;
                string _month = this.TempData["Month"] as string;
                var vm = new viewModel(context.Forklifts.ToList(), context.Packagings.ToList(), context.Transports.ToList());
                GetDate date = new GetDate();
                DateTime firstday = date.Display_first();
                DateTime lastday = date.Display_last();
                var _forklifts = from w in context.Forklifts
                                 where w.Status == "Active"
                                 && w.Location == "YSW"
                                 && w.EstimateDate_Use.CompareTo(firstday) >= 0
                                 && w.EstimateDate_Use.CompareTo(lastday) <= 0
                                 && w.PM_Review == "已审核"
                                 && w.OML_Review == "已审核"
                                 && w.Schedule=="Active"
                                 select new
                                 {
                                     ID = w.ID,
                                     ApplyId = w.ApplyId,
                                     UserName = w.UserName,
                                     Depart = w.Depart,
                                     ProjectName = w.ProjectName,
                                     ProjectNameId = w.ProjectNameId,
                                     EstimateDate_Use = w.EstimateDate_Use,
                                     EstimateTime_Start = w.EstimateTime_Start,
                                     EstimateTime_End = w.EstimateTime_End,
                                     PM_Review = w.PM_Review,
                                     OML_Review = w.OML_Review,
                                     Flag = w.Flag,
                                     Schedule = w.Schedule
                                 };
                var _packagings = from w in context.Packagings
                                  where w.Status == "Active"
                                        && w.Location == "YSW"
                                        && w.EstimateDate_Use.CompareTo(firstday) >= 0
                                        && w.EstimateDate_Use.CompareTo(lastday) <= 0
                                        && w.PM_Review == "已审核"
                                        && w.OML_Review == "已审核"
                                       && w.Schedule == "Active"
                                  select new
                                  {
                                      ID = w.ID,
                                      ApplyId = w.ApplyId,
                                      UserName = w.UserName,
                                      Depart = w.Depart,
                                      ProjectName = w.ProjectName,
                                      ProjectNameId = w.ProjectNameId,
                                      EstimateDate_Use = w.EstimateDate_Use,
                                      EstimateTime_Start = w.EstimateTime_Start,
                                      EstimateTime_End = w.EstimateTime_End,
                                      PM_Review = w.PM_Review,
                                      OML_Review = w.OML_Review,
                                      Flag = w.Flag,
                                      Schedule = w.Schedule
                                  };
                var _transports = from w in context.Transports
                                  where w.Status == "Active"
                                                && w.Location == "YSW"
                                                && w.EstimateDate_Use.CompareTo(firstday) >= 0
                                                && w.EstimateDate_Use.CompareTo(lastday) <= 0
                                                && w.PM_Review == "已审核"
                                                && w.OML_Review == "已审核"
                                                && w.Schedule == "Active"
                                  select new
                                  {
                                      ID = w.ID,
                                      ApplyId = w.ApplyId,
                                      UserName = w.UserName,
                                      Depart = w.Depart,
                                      ProjectName = w.ProjectName,
                                      ProjectNameId = w.ProjectNameId,
                                      EstimateDate_Use = w.EstimateDate_Use,
                                      EstimateTime_Start = w.EstimateTime_Start,
                                      EstimateTime_End = w.EstimateTime_End,
                                      PM_Review = w.PM_Review,
                                      OML_Review = w.OML_Review,
                                      Flag = w.Flag,
                                      Schedule = w.Schedule
                                  };
                vm.forklifts = _forklifts.ToList().Select(w => new Forklift
                {
                    ID = w.ID,
                    ApplyId = w.ApplyId,
                    UserName = w.UserName,
                    Depart = w.Depart,
                    ProjectName = w.ProjectName,
                    ProjectNameId = w.ProjectNameId,
                    EstimateDate_Use = w.EstimateDate_Use,
                    EstimateTime_Start = w.EstimateTime_Start,
                    EstimateTime_End = w.EstimateTime_End,
                    PM_Review = w.PM_Review,
                    OML_Review = w.OML_Review,
                    Flag = w.Flag,
                    Schedule = w.Schedule
                }).ToList();
                vm.packagings = _packagings.ToList().Select(w => new Packaging
                {
                    ID = w.ID,
                    ApplyId = w.ApplyId,
                    UserName = w.UserName,
                    Depart = w.Depart,
                    ProjectName = w.ProjectName,
                    ProjectNameId = w.ProjectNameId,
                    EstimateDate_Use = w.EstimateDate_Use,
                    EstimateTime_Start = w.EstimateTime_Start,
                    EstimateTime_End = w.EstimateTime_End,
                    PM_Review = w.PM_Review,
                    OML_Review = w.OML_Review,
                    Flag = w.Flag,
                    Schedule = w.Schedule
                }).ToList();
                vm.transports = _transports.ToList().Select(w => new Transport
                {
                    ID = w.ID,
                    ApplyId = w.ApplyId,
                    UserName = w.UserName,
                    Depart = w.Depart,
                    ProjectName = w.ProjectName,
                    ProjectNameId = w.ProjectNameId,
                    EstimateDate_Use = w.EstimateDate_Use,
                    EstimateTime_Start = w.EstimateTime_Start,
                    EstimateTime_End = w.EstimateTime_End,
                    PM_Review = w.PM_Review,
                    OML_Review = w.OML_Review,
                    Flag = w.Flag,
                    Schedule = w.Schedule
                }).ToList();
                return View(vm);
            }
        }
        public ActionResult sitemanagement_1_completed()
        {
            string t = DateTime.Now.ToString("yyyy-MM");
            ViewBag.Tomorrow = t;

            using (var context = new HenXinSMSContext())
            {
                var vm = new viewModel(context.Forklifts.ToList(), context.Packagings.ToList(), context.Transports.ToList());
                GetDate date = new GetDate();
                DateTime firstday = date.Display_first();
                DateTime lastday = date.Display_last();
                var _forklifts = from w in context.Forklifts
                                 where w.Status == "Active"
                                                && w.Location == "YWP"
                                                && w.EstimateDate_Use.CompareTo(firstday) >= 0 
                                                && w.EstimateDate_Use.CompareTo(lastday) <= 0
                                                && w.PM_Review == "已审核" 
                                                && w.OML_Review == "已审核"
                                                && w.Schedule == "Active"
                                 select new
                                 {
                                     ID = w.ID,
                                     ApplyId = w.ApplyId,
                                     UserName = w.UserName,
                                     Depart = w.Depart,
                                     ProjectName = w.ProjectName,
                                     ProjectNameId = w.ProjectNameId,
                                     EstimateDate_Use = w.EstimateDate_Use,
                                     EstimateTime_Start = w.EstimateTime_Start,
                                     EstimateTime_End = w.EstimateTime_End,
                                     PM_Review = w.PM_Review,
                                     OML_Review = w.OML_Review,
                                     Flag = w.Flag,
                                     Schedule = w.Schedule
                                 };
                var _packagings = from w in context.Packagings
                                  where w.Status == "Active"
                                                && w.Location == "YWP"
                                                && w.EstimateDate_Use.CompareTo(firstday) >= 0 
                                                && w.EstimateDate_Use.CompareTo(lastday) <= 0 
                                                && w.PM_Review == "已审核" 
                                                && w.OML_Review == "已审核"
                                                && w.Schedule == "Active"
                                  select new
                                  {
                                      ID = w.ID,
                                      ApplyId = w.ApplyId,
                                      UserName = w.UserName,
                                      Depart = w.Depart,
                                      ProjectName = w.ProjectName,
                                      ProjectNameId = w.ProjectNameId,
                                      EstimateDate_Use = w.EstimateDate_Use,
                                      EstimateTime_Start = w.EstimateTime_Start,
                                      EstimateTime_End = w.EstimateTime_End,
                                      PM_Review = w.PM_Review,
                                      OML_Review = w.OML_Review,
                                      Flag = w.Flag,
                                      Schedule = w.Schedule
                                  };
                var _transports = from w in context.Transports
                                  where w.Status == "Active"
                                                && w.Location == "YWP"
                                                && w.EstimateDate_Use.CompareTo(firstday) >= 0
                                                && w.EstimateDate_Use.CompareTo(lastday) <= 0 
                                                && w.PM_Review == "已审核" 
                                                && w.OML_Review == "已审核"
                                                && w.Schedule == "Active"
                                  select new
                                  {
                                      ID = w.ID,
                                      ApplyId = w.ApplyId,
                                      UserName = w.UserName,
                                      Depart = w.Depart,
                                      ProjectName = w.ProjectName,
                                      ProjectNameId = w.ProjectNameId,
                                      EstimateDate_Use = w.EstimateDate_Use,
                                      EstimateTime_Start = w.EstimateTime_Start,
                                      EstimateTime_End = w.EstimateTime_End,
                                      PM_Review = w.PM_Review,
                                      OML_Review = w.OML_Review,
                                      Flag = w.Flag,
                                      Schedule = w.Schedule
                                  };
                vm.forklifts = _forklifts.ToList().Select(w => new Forklift
                {
                    ID = w.ID,
                    ApplyId = w.ApplyId,
                    UserName = w.UserName,
                    Depart = w.Depart,
                    ProjectName = w.ProjectName,
                    ProjectNameId = w.ProjectNameId,
                    EstimateDate_Use = w.EstimateDate_Use,
                    EstimateTime_Start = w.EstimateTime_Start,
                    EstimateTime_End = w.EstimateTime_End,
                    PM_Review = w.PM_Review,
                    OML_Review = w.OML_Review,
                    Flag = w.Flag,
                    Schedule = w.Schedule
                }).ToList();
                vm.packagings = _packagings.ToList().Select(w => new Packaging
                {
                    ID = w.ID,
                    ApplyId = w.ApplyId,
                    UserName = w.UserName,
                    Depart = w.Depart,
                    ProjectName = w.ProjectName,
                    ProjectNameId = w.ProjectNameId,
                    EstimateDate_Use = w.EstimateDate_Use,
                    EstimateTime_Start = w.EstimateTime_Start,
                    EstimateTime_End = w.EstimateTime_End,
                    PM_Review = w.PM_Review,
                    OML_Review = w.OML_Review,
                    Flag = w.Flag,
                    Schedule = w.Schedule
                }).ToList();
                vm.transports = _transports.ToList().Select(w => new Transport
                {
                    ID = w.ID,
                    ApplyId = w.ApplyId,
                    UserName = w.UserName,
                    Depart = w.Depart,
                    ProjectName = w.ProjectName,
                    ProjectNameId = w.ProjectNameId,
                    EstimateDate_Use = w.EstimateDate_Use,
                    EstimateTime_Start = w.EstimateTime_Start,
                    EstimateTime_End = w.EstimateTime_End,
                    PM_Review = w.PM_Review,
                    OML_Review = w.OML_Review,
                    Flag = w.Flag,
                    Schedule = w.Schedule
                }).ToList();
                return View(vm);
            }
        }
        public ActionResult sitemanagement_1()
        {
            string t = DateTime.Now.ToString("yyyy-MM");
            ViewBag.Tomorrow = t;
            using (var context = new HenXinSMSContext())
            {
                var vm = new viewModel(context.Forklifts.ToList(), context.Packagings.ToList(), context.Transports.ToList());
                GetDate date = new GetDate();
                DateTime firstday = date.Display_first();
                DateTime lastday = date.Display_last();
                var _forklifts = from w in context.Forklifts
                                 where w.Status == "Active"
                                                && w.Location == "YWP"
                                                && w.EstimateDate_Use.CompareTo(firstday) >= 0
                                                && w.EstimateDate_Use.CompareTo(lastday) <= 0
                                                && w.PM_Review == "已审核"
                                                && w.OML_Review == "已审核"
                                                && w.Schedule == "Inactive"
                                 select new
                                 {
                                     ID = w.ID,
                                     ApplyId = w.ApplyId,
                                     UserName = w.UserName,
                                     Depart = w.Depart,
                                     ProjectName = w.ProjectName,
                                     ProjectNameId = w.ProjectNameId,
                                     EstimateDate_Use = w.EstimateDate_Use,
                                     EstimateTime_Start = w.EstimateTime_Start,
                                     EstimateTime_End = w.EstimateTime_End,
                                     PM_Review = w.PM_Review,
                                     OML_Review = w.OML_Review,
                                     Flag = w.Flag,
                                     Schedule = w.Schedule
                                 };
                var _packagings = from w in context.Packagings
                                  where w.Status == "Active"
                                                && w.Location == "YWP"
                                                && w.EstimateDate_Use.CompareTo(firstday) >= 0
                                                && w.EstimateDate_Use.CompareTo(lastday) <= 0
                                                && w.PM_Review == "已审核"
                                                && w.OML_Review == "已审核"
                                                && w.Schedule == "Inactive"
                                  select new
                                  {
                                      ID = w.ID,
                                      ApplyId = w.ApplyId,
                                      UserName = w.UserName,
                                      Depart = w.Depart,
                                      ProjectName = w.ProjectName,
                                      ProjectNameId = w.ProjectNameId,
                                      EstimateDate_Use = w.EstimateDate_Use,
                                      EstimateTime_Start = w.EstimateTime_Start,
                                      EstimateTime_End = w.EstimateTime_End,
                                      PM_Review = w.PM_Review,
                                      OML_Review = w.OML_Review,
                                      Flag = w.Flag,
                                      Schedule = w.Schedule
                                  };
                var _transports = from w in context.Transports
                                  where w.Status == "Active"
                                                && w.Location == "YWP"
                                                && w.EstimateDate_Use.CompareTo(firstday) >= 0
                                                && w.EstimateDate_Use.CompareTo(lastday) <= 0
                                                && w.PM_Review == "已审核"
                                                && w.OML_Review == "已审核"
                                                && w.Schedule == "Inactive"
                                  select new
                                  {
                                      ID = w.ID,
                                      ApplyId = w.ApplyId,
                                      UserName = w.UserName,
                                      Depart = w.Depart,
                                      ProjectName = w.ProjectName,
                                      ProjectNameId = w.ProjectNameId,
                                      EstimateDate_Use = w.EstimateDate_Use,
                                      EstimateTime_Start = w.EstimateTime_Start,
                                      EstimateTime_End = w.EstimateTime_End,
                                      PM_Review = w.PM_Review,
                                      OML_Review = w.OML_Review,
                                      Flag = w.Flag,
                                      Schedule = w.Schedule
                                  };
                vm.forklifts = _forklifts.ToList().Select(w => new Forklift
                {
                    ID = w.ID,
                    ApplyId = w.ApplyId,
                    UserName = w.UserName,
                    Depart = w.Depart,
                    ProjectName = w.ProjectName,
                    ProjectNameId = w.ProjectNameId,
                    EstimateDate_Use = w.EstimateDate_Use,
                    EstimateTime_Start = w.EstimateTime_Start,
                    EstimateTime_End = w.EstimateTime_End,
                    PM_Review = w.PM_Review,
                    OML_Review = w.OML_Review,
                    Flag = w.Flag,
                    Schedule = w.Schedule
                }).ToList();
                vm.packagings = _packagings.ToList().Select(w => new Packaging
                {
                    ID = w.ID,
                    ApplyId = w.ApplyId,
                    UserName = w.UserName,
                    Depart = w.Depart,
                    ProjectName = w.ProjectName,
                    ProjectNameId = w.ProjectNameId,
                    EstimateDate_Use = w.EstimateDate_Use,
                    EstimateTime_Start = w.EstimateTime_Start,
                    EstimateTime_End = w.EstimateTime_End,
                    PM_Review = w.PM_Review,
                    OML_Review = w.OML_Review,
                    Flag = w.Flag,
                    Schedule = w.Schedule
                }).ToList();
                vm.transports = _transports.ToList().Select(w => new Transport
                {
                    ID = w.ID,
                    ApplyId = w.ApplyId,
                    UserName = w.UserName,
                    Depart = w.Depart,
                    ProjectName = w.ProjectName,
                    ProjectNameId = w.ProjectNameId,
                    EstimateDate_Use = w.EstimateDate_Use,
                    EstimateTime_Start = w.EstimateTime_Start,
                    EstimateTime_End = w.EstimateTime_End,
                    PM_Review = w.PM_Review,
                    OML_Review = w.OML_Review,
                    Flag = w.Flag,
                    Schedule = w.Schedule
                }).ToList();
                return View(vm);
            }
        }
        public ActionResult schedule_forklift(int id)
        {
            using (var context = new HenXinSMSContext())
            {
                try
                {
                    var forklifts = context.Forklifts;
                    if (forklifts.Any())
                    {
                        var forklistToUpdate = forklifts.Find(id);
                        forklistToUpdate.Schedule = "Active";
                        context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    return RedirectToAction("sitemanagement", new { id = id, saveChangesError = true });
                }
                return RedirectToAction("sitemanagement");
            }
        }
        public ActionResult schedule_packaging(int id)
        {
            using (var context = new HenXinSMSContext())
            {
                try
                {
                    var packagings = context.Packagings;
                    if (packagings.Any())
                    {
                        var packagingToUpdate = packagings.Find(id);
                        packagingToUpdate.Schedule = "Active";
                        context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    return RedirectToAction("sitemanagement", new { id = id, saveChangesError = true });
                }
                return RedirectToAction("sitemanagement");
            }
        }
        public ActionResult schedule_transport(int id)
        {
            using (var context = new HenXinSMSContext())
            {
                try
                {
                    var transports = context.Transports;
                    if (transports.Any())
                    {
                        var transportToUpdate = transports.Find(id);
                        transportToUpdate.Schedule = "Active";
                        context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    return RedirectToAction("sitemanagement", new { id = id, saveChangesError = true });
                }
                return RedirectToAction("sitemanagement");
            }
        }
    }
}
