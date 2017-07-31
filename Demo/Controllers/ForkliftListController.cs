﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.ComponentModel;
using HengXinSMS.Models;
using PagedList;
using System.IO;
namespace HengXinSMS.Controllers
{
    [MemberCheck]
    public class ForkliftListController : Controller
    {
        public ActionResult forkliftlist(string sortOrder, string searchString, string currentFilter, int? page)
        {
            using (var context = new HenXinSMSContext())
            {
                ViewBag.ProjectNameSortParm = String.IsNullOrEmpty(sortOrder) ? "first_desc" : "";
                ViewBag.ApplicationDate = sortOrder == "last" ? "last_desc" : "last";
                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }
                ViewBag.CurrentFilter = searchString;
                var forklifts = from w in context.Forklifts
                                where w.Status == "Active" 
                                && w.Schedule == "Inactive"
                                && w.OML_Review=="未审核"

                                select w;
                if (!string.IsNullOrEmpty(searchString))
                {
                    forklifts = forklifts.Where(w => w.UserName.Contains(searchString) 
                                               || w.ProjectName.Contains(searchString) 
                                               || w.Location.Contains(searchString) 
                                               || w.OML_Review.Contains(searchString) 
                                               || w.PM_Review.Contains(searchString) 
                                               || w.Depart.Contains(searchString)
                                               || w.ProjectNameId.Contains(searchString));

                }
                switch (sortOrder)
                {
                    case "first_desc":
                        forklifts = forklifts.OrderByDescending(w => w.ProjectName);
                        break;
                    case "last_desc":
                        forklifts = forklifts.OrderByDescending(w => w.ApplicationDate);
                        break;
                    case "last":
                        forklifts = forklifts.OrderBy(w => w.ApplicationDate);
                        break;
                    default:
                        forklifts = forklifts.OrderBy(w => w.ProjectName);
                        break;
                }
                int pageSize = 6;
                int pageNumber = (page ?? 1);
                return View(forklifts.ToPagedList(pageNumber, pageSize));
            }
        }
        public ActionResult projectlist(string sortOrder, string searchString, string currentFilter, int? page)
        {
            using (var context = new HenXinSMSContext())
            {
                ViewBag.UserNameSortParm = String.IsNullOrEmpty(sortOrder) ? "first_desc" : "";
                ViewBag.DepartSortParm = sortOrder == "last" ? "last_desc" : "last";
                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }
                ViewBag.CurrentFilter = searchString;


                string _projectManager = Session["UserName"].ToString();
                string _level = Session["Level"].ToString();
                if (_level != "管理员")
                {
                    var projects = from w in context.Projects
                                   where w.ProjectManager == _projectManager
                                   select w;
                    if (!string.IsNullOrEmpty(searchString))
                    {
                        projects = projects.Where(w => w.ProjectManager.Contains(searchString)
                                                  || w.ProjectNameId.Contains(searchString)
                                                  || w.ProjectName.Contains(searchString)
                                                  || w.Members.Contains(searchString)
                                                  || w.InsertOfTime.ToString().Contains(searchString));

                    }
                    switch (sortOrder)
                    {
                        case "first_desc":
                            projects = projects.OrderByDescending(w => w.ProjectName);
                            break;
                        case "last_desc":
                            projects = projects.OrderByDescending(w => w.ProjectNameId);
                            break;
                        case "last":
                            projects = projects.OrderBy(w => w.ProjectNameId);
                            break;
                        default:
                            projects = projects.OrderBy(w => w.ProjectName);
                            break;
                    }
                    int pageSize = 7;
                    int pageNumber = (page ?? 1);
                    return View(projects.ToPagedList(pageNumber, pageSize));
                }
                else
                {
                    var projects = from w in context.Projects
                                   select w;
                    if (!string.IsNullOrEmpty(searchString))
                    {
                        projects = projects.Where(w => w.ProjectManager.Contains(searchString)
                                                  || w.ProjectNameId.Contains(searchString)
                                                  || w.ProjectName.Contains(searchString)
                                                  || w.Members.Contains(searchString)
                                                  || w.InsertOfTime.ToString().Contains(searchString));
                    }
                    switch (sortOrder)
                    {
                        case "first_desc":
                            projects = projects.OrderByDescending(w => w.ProjectName);
                            break;
                        case "last_desc":
                            projects = projects.OrderByDescending(w => w.ProjectNameId);
                            break;
                        case "last":
                            projects = projects.OrderBy(w => w.ProjectNameId);
                            break;
                        default:
                            projects = projects.OrderBy(w => w.ProjectName);
                            break;
                    }
                    int pageSize = 7;
                    int pageNumber = (page ?? 1);
                    return View(projects.ToPagedList(pageNumber, pageSize));
                }
            }
        }
        public ActionResult newforklift()
        {

            return View();
        }
        [HttpPost]
        public ActionResult addforklift()
        {
            GetDate date = new GetDate();
            using (var context = new HenXinSMSContext())
            {
                var _username = Request["UserName"];
                var _depart=Request["Depart"];
                var _projectname = Request["ProjectName"];
                var _projectnameid = Request["ProjectNameId"];
                var _projectmanager = Request["ProjectManager"];
                var _estimateDate_use =Convert.ToDateTime( Request["EstimateDate_Use"]);
                var _location = Request["Location"];
                var _estimatetime_start =Convert.ToDateTime( Request["EstimateTime_Start"]);
                var _estimatetime_end =Convert.ToDateTime( Request["EstimateTime_End"]);
                var _serverequest = Request["ServeRequest"];
                var _numofforklift = Request["NumofForklift"];
                var _numofhuman = Request["NumofHuman"];
                var _purpose = Request["Purpose"];
                var _amount = Request["Amount"];
                var _annex  = Request["Annex"];
                int n = date.days(_estimateDate_use);
                var forklift = new Forklift
                {
                    UserName = _username,
                    Depart = _depart,
                    ProjectName = _projectname,
                    ProjectNameId = _projectnameid,
                    ProjectManager = _projectmanager,
                    EstimateDate_Use = _estimateDate_use,
                    Location = _location,
                    EstimateTime_Start = _estimatetime_start.AddDays(n),
                    EstimateTime_End = _estimatetime_end.AddDays(n),
                    ServeRequest = _serverequest,
                    NumofForklift = _numofforklift,
                    NumofHuman = _numofhuman,
                    Purpose = _purpose,
                    Annex = _annex,
                    Amount = _amount
                };
                context.Forklifts.Add(forklift);
                context.SaveChanges();
                var _forklifts = context.Forklifts;
                var _forklift = _forklifts.First(d => d.EstimateDate_Use==_estimateDate_use );
                if (_forklift != null)
                {
                    return Content("Success");

                }
                else
                {
                    return Content("Error");
                }
            }
        }
        public ActionResult uploadfile()
        {
            return View();
        }
        [HttpPost]
        public ActionResult uploadfile(HttpPostedFileBase file)
        {
            var fileName = file.FileName;
            var filePath = Server.MapPath(string.Format("~/{0}", "Forklifts"));
            file.SaveAs(Path.Combine(filePath, fileName));
            return View();
            
        }
        public ActionResult delete(int id)
        {
            using (var context = new HenXinSMSContext())
            {
                try
                {
                    Forklift forkliftsToDelete = new Forklift() { ID = id };
                    context.Entry(forkliftsToDelete).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    return RedirectToAction("forkliftlist", new { id = id, saveChangesError = true });
                }
                return RedirectToAction("forkliftlist");
            }
        }
         public ActionResult details(int? id)
         {
             using (var context = new HenXinSMSContext())
             {
                 if (id == null)
                 {
                     return new HttpStatusCodeResult(404, "请求错误");
                 }
                 Forklift forklift = context.Forklifts.Find(id);
                 if (forklift == null)
                 {
                     return HttpNotFound();
                 }
                 return View(forklift);
             }
         }
         public ActionResult particulars(int? id)
         {
             using (var context = new HenXinSMSContext())
             {
                 if (id == null)
                 {
                     return new HttpStatusCodeResult(404, "请求错误");
                 }
                 Forklift forklift = context.Forklifts.Find(id);
                 if (forklift == null)
                 {
                     return HttpNotFound();
                 }
                 return View(forklift);
             }
         }
         public ActionResult edit(int? id)
         {
             using (var context = new HenXinSMSContext())
             {
                 if (id == null)
                 {
                     return new HttpStatusCodeResult(404, "请求错误");
                 }
                 Forklift forklift = context.Forklifts.Find(id);
                 if (forklift == null)
                 {
                     return HttpNotFound();
                 }
                 return View(forklift);
             }
         }

         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult edit([Bind(Include = "ID,UserName,Depart, ProjectName,ProjectNameId,ProjectManager, ApplicationDate, EstimateDate_Use,Location,EstimateTime_Start,EstimateTime_End,ServeRequest,NumofForklift, NumofHuman,Purpose,Amount,PM_Review,OML_Review,Flag,Status,Schedule,ApplyId,CostofLabor,CostofForklist, Annex ")] Forklift forklift)
         {
             using (var context = new HenXinSMSContext())
             {
                 if (ModelState.IsValid)
                 {
                     context.Entry(forklift).State = EntityState.Modified;
                     context.SaveChanges();
                     return RedirectToAction("forkliftlist");
                 }
                 return View(forklift);
             }
         }
         public ActionResult check(int id)
         {
          
             GetDate date = new GetDate();
             using (var context = new HenXinSMSContext())
             {
                 try
                 {
                     var forklifts = context.Forklifts;
                     if (forklifts.Any())
                     {
                         var forklistToUpdate = forklifts.Find(id);
                         forklistToUpdate.OML_Review = "已审核";
                         forklistToUpdate.ApplyId = "F" + date.Display();
                         context.SaveChanges();
                     }
                 }
                 catch (Exception e)
                 {
                     return RedirectToAction("forkliftlist", new { id = id, saveChangesError = true });
                 }
                 return RedirectToAction("successforklift");
             }
         }
         public ActionResult get(int id)
         {
             using (var context = new HenXinSMSContext())
             {
                 try
                 {
                     var projects = context.Projects;
                     if (projects.Any())
                     {
                         var projectGetProjectId = projects.Find(id);
                         Session["ProjectName"] = projectGetProjectId.ProjectName;
                         Session["ProjectId"] = projectGetProjectId.ProjectNameId;
                         Session["ProjectManager"] = projectGetProjectId.ProjectManager;
                         context.SaveChanges();
                     }
                 }
                 catch (Exception e)
                 {
                     return RedirectToAction("newforklift", new { id = id, saveChangesError = true });
                 }
                 return RedirectToAction("newforklift");
             }
         }
        public ActionResult checkforklift(string sortOrder, string searchString, string currentFilter, int? page)
        {
            using (var context = new HenXinSMSContext())
            {
                ViewBag.ProjectNameSortParm = String.IsNullOrEmpty(sortOrder) ? "first_desc" : "";
                ViewBag.ApplicationDate = sortOrder == "last" ? "last_desc" : "last";
                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }
                ViewBag.CurrentFilter = searchString;


                var forklifts = from w in context.Forklifts
                                where w.PM_Review == "已审核" && w.OML_Review != "已审核" && w.Status == "Active" && w.Schedule == "Inactive"
                                select w;
                if (!string.IsNullOrEmpty(searchString))
                {
                    forklifts = forklifts.Where(w => w.UserName.Contains(searchString) || w.ProjectName.Contains(searchString) || w.Location.Contains(searchString) || w.OML_Review.Contains(searchString) || w.PM_Review.Contains(searchString) || w.Depart.Contains(searchString));

                }
                switch (sortOrder)
                {
                    case "first_desc":
                        forklifts = forklifts.OrderByDescending(w => w.ProjectName);
                        break;
                    case "last_desc":
                        forklifts = forklifts.OrderByDescending(w => w.ApplicationDate);
                        break;
                    case "last":
                        forklifts = forklifts.OrderBy(w => w.ApplicationDate);
                        break;
                    default:
                        forklifts = forklifts.OrderBy(w => w.ProjectName);
                        break;
                }
                int pageSize = 6;
                int pageNumber = (page ?? 1);
                return View(forklifts.ToPagedList(pageNumber, pageSize));

            }
        }
        public ActionResult successforklift(string sortOrder, string searchString, string currentFilter, int? page)
        {
            using (var context = new HenXinSMSContext())
            {

                ViewBag.ProjectNameSortParm = String.IsNullOrEmpty(sortOrder) ? "first_desc" : "";
                ViewBag.ApplicationDate = sortOrder == "last" ? "last_desc" : "last";
                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }
                ViewBag.CurrentFilter = searchString;


                var forklifts = from w in context.Forklifts
                                where w.PM_Review == "已审核" 
                                && w.OML_Review == "已审核"
                                && w.Schedule == "Inactive"
                                select w;
                if (!string.IsNullOrEmpty(searchString))
                {
                    forklifts = forklifts.Where
                        (w => w.UserName.Contains(searchString) || w.ProjectName.Contains(searchString) || w.ApplyId.Contains(searchString) || w.ApplicationDate.ToString().Contains(searchString));

                }
                switch (sortOrder)
                {
                    case "first_desc":
                        forklifts = forklifts.OrderByDescending(w => w.ProjectName);
                        break;
                    case "last_desc":
                        forklifts = forklifts.OrderByDescending(w => w.ApplicationDate);
                        break;
                    case "last":
                        forklifts = forklifts.OrderBy(w => w.ApplicationDate);
                        break;
                    default:
                        forklifts = forklifts.OrderBy(w => w.ProjectName);
                        break;
                }
                int pageSize = 6;
                int pageNumber = (page ?? 1);
                return View(forklifts.ToPagedList(pageNumber, pageSize));
            }
        }
        public ActionResult after_Schedule(string sortOrder, string searchString, string currentFilter, int? page)
        {
            using (var context = new HenXinSMSContext())
            {
                ViewBag.ProjectNameSortParm = String.IsNullOrEmpty(sortOrder) ? "first_desc" : "";
                ViewBag.ApplicationDate = sortOrder == "last" ? "last_desc" : "last";
                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }
                ViewBag.CurrentFilter = searchString;
                var forklifts = from w in context.Forklifts
                                where w.Status == "Active"&&w.Schedule=="Active"
                                &&w.CostofForklist+w.CostofLabor+w.Other==0
                                select w;
                if (!string.IsNullOrEmpty(searchString))
                {
                    forklifts = forklifts.Where(w => w.UserName.Contains(searchString)
                                               || w.ProjectName.Contains(searchString)
                                               || w.Location.Contains(searchString)
                                               || w.ApplyId.Contains(searchString)
                                               || w.ProjectNameId.Contains(searchString));

                }
                switch (sortOrder)
                {
                    case "first_desc":
                        forklifts = forklifts.OrderByDescending(w => w.ProjectName);
                        break;
                    case "last_desc":
                        forklifts = forklifts.OrderByDescending(w => w.ApplicationDate);
                        break;
                    case "last":
                        forklifts = forklifts.OrderBy(w => w.ApplicationDate);
                        break;
                    default:
                        forklifts = forklifts.OrderBy(w => w.ProjectName);
                        break;
                }
                int pageSize = 8;
                int pageNumber = (page ?? 1);
                return View(forklifts.ToPagedList(pageNumber, pageSize));
            }
        }
        public ActionResult after_Schedule_1(string sortOrder, string searchString, string currentFilter, int? page)
        {
            using (var context = new HenXinSMSContext())
            {
                ViewBag.ProjectNameSortParm = String.IsNullOrEmpty(sortOrder) ? "first_desc" : "";
                ViewBag.ApplicationDate = sortOrder == "last" ? "last_desc" : "last";
                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }
                ViewBag.CurrentFilter = searchString;
                var forklifts = from w in context.Forklifts
                                where w.Status == "Active" && w.Schedule == "Active"
                                && w.CostofForklist + w.CostofLabor + w.Other != 0
                                select w;
                if (!string.IsNullOrEmpty(searchString))
                {
                    forklifts = forklifts.Where(w => w.UserName.Contains(searchString)
                                               || w.ProjectName.Contains(searchString)
                                               || w.Location.Contains(searchString)
                                               || w.ApplyId.Contains(searchString)
                                               || w.ProjectNameId.Contains(searchString));

                }
                switch (sortOrder)
                {
                    case "first_desc":
                        forklifts = forklifts.OrderByDescending(w => w.ProjectName);
                        break;
                    case "last_desc":
                        forklifts = forklifts.OrderByDescending(w => w.ApplicationDate);
                        break;
                    case "last":
                        forklifts = forklifts.OrderBy(w => w.ApplicationDate);
                        break;
                    default:
                        forklifts = forklifts.OrderBy(w => w.ProjectName);
                        break;
                }
                int pageSize = 8;
                int pageNumber = (page ?? 1);
                return View(forklifts.ToPagedList(pageNumber, pageSize));
            }
        }
        public ActionResult cost(int? id)
        {
            using (var context = new HenXinSMSContext())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(404, "请求错误");
                }
                Forklift forklift = context.Forklifts.Find(id);
                if (forklift == null)
                {
                    return HttpNotFound();
                }
                return View(forklift);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult cost( Forklift forklift)
        {
            using (var context = new HenXinSMSContext())
            {
                if (ModelState.IsValid)
                {
                    context.Entry(forklift).State = EntityState.Modified;
                    context.SaveChanges();
                    return RedirectToAction("after_Schedule");
                }
                return View(forklift);
            }
        }

        public ActionResult forklift_rdlc()
        {
            return View();
        }
        public ActionResult forkliftlistOfysw(string sortOrder, string searchString, string currentFilter, int? page)
        {
            using (var context = new HenXinSMSContext())
            {
                ViewBag.ProjectNameSortParm = String.IsNullOrEmpty(sortOrder) ? "first_desc" : "";
                ViewBag.ApplicationDate = sortOrder == "last" ? "last_desc" : "last";
                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }
                ViewBag.CurrentFilter = searchString;
                var forklifts = from w in context.Forklifts
                                where w.Status == "Active"
                              
                                && w.OML_Review == "已审核"
                                && w.PM_Review == "已审核"
                                && w.Location == "YSW"
                                select w;
                if (!string.IsNullOrEmpty(searchString))
                {
                    forklifts = forklifts.Where(w => w.UserName.Contains(searchString)
                                               || w.ProjectName.Contains(searchString)
                                               || w.ApplyId.Contains(searchString)
                                               || w.ApplicationDate.ToString().Contains(searchString)
                                               || w.Schedule.Contains(searchString)
                                               || w.ProjectNameId.Contains(searchString));
                                               


                }
                switch (sortOrder)
                {
                    case "first_desc":
                        forklifts = forklifts.OrderByDescending(w => w.ProjectName);
                        break;
                    case "last_desc":
                        forklifts = forklifts.OrderByDescending(w => w.ApplicationDate);
                        break;
                    case "last":
                        forklifts = forklifts.OrderBy(w => w.ApplicationDate);
                        break;
                    default:
                        forklifts = forklifts.OrderBy(w => w.ProjectName);
                        break;
                }
                int pageSize = 6;
                int pageNumber = (page ?? 1);
                return View(forklifts.ToPagedList(pageNumber, pageSize));
            }
        }
        public ActionResult forkliftlistOfywp(string sortOrder, string searchString, string currentFilter, int? page)
        {
            using (var context = new HenXinSMSContext())
            {
                ViewBag.ProjectNameSortParm = String.IsNullOrEmpty(sortOrder) ? "first_desc" : "";
                ViewBag.ApplicationDate = sortOrder == "last" ? "last_desc" : "last";
                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }
                ViewBag.CurrentFilter = searchString;
                var forklifts = from w in context.Forklifts
                                where w.Status == "Active"
                            
                                && w.OML_Review == "已审核"
                                && w.PM_Review == "已审核"
                                && w.Location == "YWP"
                                select w;
                if (!string.IsNullOrEmpty(searchString))
                {
                    forklifts = forklifts.Where(w => w.UserName.Contains(searchString)
                                               || w.ProjectName.Contains(searchString)
                                               || w.ApplyId.Contains(searchString)
                                               || w.ApplicationDate.ToString().Contains(searchString)
                                               || w.Schedule.Contains(searchString)
                                               || w.ProjectNameId.Contains(searchString));

                }
                switch (sortOrder)
                {
                    case "first_desc":
                        forklifts = forklifts.OrderByDescending(w => w.ProjectName);
                        break;
                    case "last_desc":
                        forklifts = forklifts.OrderByDescending(w => w.ApplicationDate);
                        break;
                    case "last":
                        forklifts = forklifts.OrderBy(w => w.ApplicationDate);
                        break;
                    default:
                        forklifts = forklifts.OrderBy(w => w.ProjectName);
                        break;
                }
                int pageSize = 6;
                int pageNumber = (page ?? 1);
                return View(forklifts.ToPagedList(pageNumber, pageSize));
            }
        }
        public ActionResult schedule_forklift_ysw(int id)
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
                    return RedirectToAction("forkliftlistOfysw", new { id = id, saveChangesError = true });
                }
                return RedirectToAction("forkliftlistOfysw");
            }
        }
        public ActionResult schedule_forklift_ywp(int id)
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
                    return RedirectToAction("forkliftlistOfywp", new { id = id, saveChangesError = true });
                }
                return RedirectToAction("forkliftlistOfywp");
            }
        }
    }
}
