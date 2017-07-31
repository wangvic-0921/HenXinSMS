using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.ComponentModel;
using HengXinSMS.Models;
using PagedList;
namespace HengXinSMS.Controllers
{
    [MemberCheck]
    public class ProjectListController : Controller
    {
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
                var projects = from w in context.Projects
                            select w;
                if (!string.IsNullOrEmpty(searchString))
                {
                    projects = projects.Where(w => w.ProjectManager.Contains(searchString) 
                                              || w.ProjectNameId.Contains(searchString) 
                                              || w.ProjectName.Contains(searchString)
                                              ||w.Members.Contains(searchString)
                                              ||w.InsertOfTime.ToString().Contains(searchString));
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
        public ActionResult newproject()
        { 
            return View();
        }
        [HttpPost]
        public ActionResult addproject()
        {
             using (var context = new HenXinSMSContext())
                {
                    string _projectmanager = Request["ProjectManager"];
                    string _projectname = Request["ProjectName"];
                    string _projectnameid = Request["ProjectNameId"];
                    string _members = Request["Members"];
                    var project = new Project { ProjectManager = _projectmanager, ProjectName = _projectname, ProjectNameId = _projectnameid,Members=_members };
                    context.Projects.Add(project);
                    context.SaveChanges();
                    var _projects = context.Projects;
                    var _project = _projects.First(d => d.ProjectNameId == _projectnameid);
                    if (_project != null)
                    {
                        return Content("Success");
                    }
                    else
                    {
                        return Content("Error");
                    }
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
                Project project = context.Projects.Find(id);
                if (project == null)
                {
                    return HttpNotFound();
                }
                return View(project);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult edit([Bind(Include = "ID,ProjectManager, ProjectName, ProjectNameId,Members,InsertOfTime ")] Project project)
        {
            using (var context = new HenXinSMSContext())
            {
                if (ModelState.IsValid)
                {
                    context.Entry(project).State = EntityState.Modified;
                    context.SaveChanges();
                    return RedirectToAction("projectlist");
                }
                return View(project);
            }
        }
        public ActionResult delete(int id)
        {
            using (var context = new HenXinSMSContext())
            {
                try
                {
                    Project projectToDelete = new Project() { ID = id };
                    context.Entry(projectToDelete).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    return RedirectToAction(" projectlist", new { id = id, saveChangesError = true });
                }
                return RedirectToAction("projectlist");
            }
        }
    }
}
