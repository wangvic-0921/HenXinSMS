using System;
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
    public class PackagingList_PmController : Controller
    {
        //
        // GET: /PackagingList_Pm/

        public ActionResult packaginglist(string sortOrder, string searchString, string currentFilter, int? page)
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

                string _projectManager = Session["UserName"].ToString();
                var packagings = from w in context.Packagings
                                 where w.Status == "Active"
                                      && w.Schedule == "Inactive"
                                      && w.ProjectManager == _projectManager
                                      && w.OML_Review == "未审核"
                                      && w.PM_Review == "未审核"
                                 select w;
                if (!string.IsNullOrEmpty(searchString))
                {
                    packagings = packagings.Where(w => w.UserName.Contains(searchString) || w.ProjectName.Contains(searchString) || w.Location.Contains(searchString) || w.OML_Review.Contains(searchString) || w.PM_Review.Contains(searchString));

                }
                switch (sortOrder)
                {
                    case "first_desc":
                        packagings = packagings.OrderByDescending(w => w.ProjectName);
                        break;
                    case "last_desc":
                        packagings = packagings.OrderByDescending(w => w.ApplicationDate);
                        break;
                    case "last":
                        packagings = packagings.OrderBy(w => w.ApplicationDate);
                        break;
                    default:
                        packagings = packagings.OrderBy(w => w.ProjectName);
                        break;
                }
                int pageSize = 6;
                int pageNumber = (page ?? 1);
                return View(packagings.ToPagedList(pageNumber, pageSize));
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
                    return RedirectToAction("newpackaging", new { id = id, saveChangesError = true });
                }
                return RedirectToAction("newpackaging");
            }
        }
        public ActionResult newpackaging()
        {

            return View();
        }
        [HttpPost]
        public ActionResult addpackaging()
        {
            GetDate date = new GetDate();
            using (var context = new HenXinSMSContext())
            {
                var _username = Request["UserName"];
                var _depart = Request["Depart"];
                var _projectname = Request["ProjectName"];
                var _projectnameid = Request["ProjectNameId"];
                var _projectmanager = Request["ProjectManager"];
                var _estimateDate_use = Convert.ToDateTime(Request["EstimateDate_Use"]);
                var _location = Request["Location"];
                var _estimatetime_start = Convert.ToDateTime(Request["EstimateTime_Start"]);
                var _estimatetime_end = Convert.ToDateTime(Request["EstimateTime_End"]);
                var _packageofrequest = Request["PackageofRequest"];
                var _packageofsize = Request["PackageofSize"];
                var _numofpackage = Request["NumofPackage"];
                var _trgofpackage = Request["TrgofPackage"];
                var _annex = Request["Annex"];
                int n = date.days(_estimateDate_use);
                var packaging = new Packaging

                {
                    UserName = _username,
                    Depart = _depart,
                    ProjectName = _projectname,
                    ProjectNameId = _projectnameid,
                    ProjectManager = _projectmanager,
                    EstimateDate_Use =_estimateDate_use,
                    Location = _location,
                    EstimateTime_Start = _estimatetime_start.AddDays(n),
                    EstimateTime_End = _estimatetime_end.AddDays(n),
                    PackageofRequest = _packageofrequest,
                    PackageofSize = _packageofsize,
                    NumofPackage = _numofpackage,
                    Annex = _annex,
                    TrgofPackage = _trgofpackage
                };
                context.Packagings.Add(packaging);
                context.SaveChanges();
                var _packagings = context.Packagings;
                var _packaging = _packagings.First(d => d.EstimateDate_Use == _estimateDate_use);
                if (_packaging != null)
                {
                    return Content("Success");
                }
                else
                {
                    return Content("Error");
                }
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
                Packaging packaging = context.Packagings.Find(id);
                if (packaging == null)
                {
                    return HttpNotFound();
                }
                return View(packaging);
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
                Packaging packaging = context.Packagings.Find(id);
                if (packaging == null)
                {
                    return HttpNotFound();
                }
                return View(packaging);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult edit([Bind(Include = "ID,UserName,Depart, ProjectName,ProjectNameId,ProjectManager, ApplicationDate, EstimateDate_Use,EstimateTime_Start,EstimateTime_End,Location, PackageofRequest,PackageofSize, NumofPackage , TrgofPackage,PM_Review,OML_Review,Flag,Status,Schedule, Annex")] Packaging packaging)
        {
            using (var context = new HenXinSMSContext())
            {
                if (ModelState.IsValid)
                {
                    context.Entry(packaging).State = EntityState.Modified;
                    context.SaveChanges();
                    return RedirectToAction("packaginglist");
                }
                return View(packaging);
            }
        }
        public ActionResult check(int id)
        {
            using (var context = new HenXinSMSContext())
            {
                try
                {
                    var packagings = context.Packagings;
                    if (packagings.Any())
                    {
                        var packagingToUpdate = packagings.Find(id);
                        packagingToUpdate.PM_Review = "已审核";
                        context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    return RedirectToAction("packaginglist", new { id = id, saveChangesError = true });
                }
                return RedirectToAction("after_PM_check");
            }
        }
        public ActionResult delete(int id)
        {
            using (var context = new HenXinSMSContext())
            {
                try
                {
                    Packaging packagesToDelete = new Packaging() { ID = id };
                    context.Entry(packagesToDelete).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    return RedirectToAction("packaginglist", new { id = id, saveChangesError = true });
                }
                return RedirectToAction("packaginglist");
            }
        }
        public ActionResult successpackaging(string sortOrder, string searchString, string currentFilter, int? page)
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
                string _projectManager = Session["UserName"].ToString();
                var packagings = from w in context.Packagings
                                 where w.ProjectManager == _projectManager
                                       &&w.PM_Review == "已审核"
                                       && w.OML_Review == "已审核"
                                       && w.Schedule == "Inactive"
                                 select w;
                if (!string.IsNullOrEmpty(searchString))
                {
                    packagings = packagings.Where(w => w.UserName.Contains(searchString) || w.ProjectName.Contains(searchString) || w.Location.Contains(searchString) || w.OML_Review.Contains(searchString) || w.PM_Review.Contains(searchString));

                }
                switch (sortOrder)
                {
                    case "first_desc":
                        packagings = packagings.OrderByDescending(w => w.ProjectName);
                        break;
                    case "last_desc":
                        packagings = packagings.OrderByDescending(w => w.ApplicationDate);
                        break;
                    case "last":
                        packagings = packagings.OrderBy(w => w.ApplicationDate);
                        break;
                    default:
                        packagings = packagings.OrderBy(w => w.ProjectName);
                        break;
                }
                int pageSize = 6;
                int pageNumber = (page ?? 1);
                return View(packagings.ToPagedList(pageNumber, pageSize));
            }
        }
        public ActionResult after_PM_check(string sortOrder, string searchString, string currentFilter, int? page)
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

                   string _projectManager = Session["UserName"].ToString();
                   var packagings = from w in context.Packagings
                                    where w.Status == "Active"
                                         && w.Schedule == "Inactive"
                                         && w.ProjectManager == _projectManager
                                         && w.OML_Review == "未审核"
                                         && w.PM_Review == "已审核"
                                    select w;
                   if (!string.IsNullOrEmpty(searchString))
                   {
                       packagings = packagings.Where(w => w.UserName.Contains(searchString) || w.ProjectName.Contains(searchString) || w.Location.Contains(searchString) || w.OML_Review.Contains(searchString) || w.PM_Review.Contains(searchString));

                   }
                   switch (sortOrder)
                   {
                       case "first_desc":
                           packagings = packagings.OrderByDescending(w => w.ProjectName);
                           break;
                       case "last_desc":
                           packagings = packagings.OrderByDescending(w => w.ApplicationDate);
                           break;
                       case "last":
                           packagings = packagings.OrderBy(w => w.ApplicationDate);
                           break;
                       default:
                           packagings = packagings.OrderBy(w => w.ProjectName);
                           break;
                   }
                   int pageSize = 6;
                   int pageNumber = (page ?? 1);
                   return View(packagings.ToPagedList(pageNumber, pageSize));
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
                string _projectManager = Session["UserName"].ToString();
                var packagings = from w in context.Packagings
                                 where w.ProjectManager == _projectManager
                                 && w.Status == "Active" 
                                 && w.Schedule == "Active"
                                 select w;
                if (!string.IsNullOrEmpty(searchString))
                {
                    packagings = packagings.Where(w => w.UserName.Contains(searchString)
                                              || w.ProjectName.Contains(searchString)
                                              || w.Location.Contains(searchString)
                                              || w.ApplyId.Contains(searchString)
                                              || w.ProjectNameId.Contains(searchString));

                }
                switch (sortOrder)
                {
                    case "first_desc":
                        packagings = packagings.OrderByDescending(w => w.ProjectName);
                        break;
                    case "last_desc":
                        packagings = packagings.OrderByDescending(w => w.ApplicationDate);
                        break;
                    case "last":
                        packagings = packagings.OrderBy(w => w.ApplicationDate);
                        break;
                    default:
                        packagings = packagings.OrderBy(w => w.ProjectName);
                        break;
                }
                int pageSize = 6;
                int pageNumber = (page ?? 1);
                return View(packagings.ToPagedList(pageNumber, pageSize));

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
            var filePath = Server.MapPath(string.Format("~/{0}", "Packagings"));
            file.SaveAs(Path.Combine(filePath, fileName));
            return View();
        }
    }
}
