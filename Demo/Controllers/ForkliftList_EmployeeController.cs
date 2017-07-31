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
    public class ForkliftList_EmployeeController : Controller
    {
        //
        // GET: /ForkliftList_Employe/

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

                string _username = Session["UserName"].ToString();
                var forklifts = from w in context.Forklifts
                                where w.UserName == _username
                                       && w.PM_Review == "未审核"
                                       && w.OML_Review == "未审核"
                                       && w.Status == "Active"
                                       && w.Schedule == "Inactive"
                                select w;
                if (!string.IsNullOrEmpty(searchString))
                {
                    forklifts = forklifts.Where(w => w.UserName.Contains(searchString) || w.ProjectName.Contains(searchString) || w.Location.Contains(searchString)||w.OML_Review.Contains(searchString)||w.PM_Review.Contains(searchString));

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
                int pageSize = 7;
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
                string _username = Session["UserName"].ToString();
                var projects = from w in context.Projects
                               select w;
                projects = projects.Where(w => w.Members.Contains(_username));
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
                    return RedirectToAction("newforklift", new { id = id, saveChangesError = true });
                }
                return RedirectToAction("newforklift");
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
                var _depart = Request["Depart"];
                var _projectname = Request["ProjectName"];
                var _projectnameid = Request["ProjectNameId"];
                var _projectmanager = Request["ProjectManager"];
                var _estimateDate_use = Convert.ToDateTime(Request["EstimateDate_Use"]);
                var _location = Request["Location"];
                var _estimatetime_start = Convert.ToDateTime(Request["EstimateTime_Start"]);
                var _estimatetime_end = Convert.ToDateTime(Request["EstimateTime_End"]);
                var _serverequest = Request["ServeRequest"];
                var _numofforklift = Request["NumofForklift"];
                var _numofhuman = Request["NumofHuman"];
                var _purpose = Request["Purpose"];
                var _amount = Request["Amount"];
                var _annex = Request["Annex"];
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
                var _forklift = _forklifts.First(d => d.EstimateDate_Use == _estimateDate_use);
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
                string _username = Session["UserName"].ToString();
                var forklifts = from w in context.Forklifts
                                where w.UserName==_username 
                                &&    w.PM_Review == "已审核"
                                &&    w.OML_Review == "已审核"
                                &&    w.Schedule == "Inactive"
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
                string _username = Session["UserName"].ToString();
                var forklifts = from w in context.Forklifts
                                where w.UserName == _username 
                                &&    w.Status == "Active" 
                                &&    w.Schedule == "Active"
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
                int pageSize = 6;
                int pageNumber = (page ?? 1);
                return View(forklifts.ToPagedList(pageNumber, pageSize));
            }
        }
    }
}
