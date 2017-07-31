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
    public class TransportListController : Controller
    {
        public ActionResult transportlist(string sortOrder, string searchString, string currentFilter, int? page)
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


                var transports = from w in context.Transports
                                 where w.Status == "Active"
                               && w.Schedule == "Inactive"
                               && w.OML_Review == "未审核"
                            
                                 select w;
                if (!string.IsNullOrEmpty(searchString))
                {
                    transports = transports.Where(w => w.UserName.Contains(searchString) || w.ProjectName.Contains(searchString) ||w.OML_Review.Contains(searchString) || w.PM_Review.Contains(searchString));

                }
                switch (sortOrder)
                {
                    case "first_desc":
                        transports = transports.OrderByDescending(w => w.ProjectName);
                        break;
                    case "last_desc":
                        transports = transports.OrderByDescending(w => w.ApplicationDate);
                        break;
                    case "last":
                        transports = transports.OrderBy(w => w.ApplicationDate);
                        break;
                    default:
                        transports = transports.OrderBy(w => w.ProjectName);
                        break;
                }
                int pageSize = 6;
                int pageNumber = (page ?? 1);
                return View(transports.ToPagedList(pageNumber, pageSize));

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
        public ActionResult newtransport()
        {

            return View();
        }
        [HttpPost]
        public ActionResult addtransport()
        {
            GetDate date = new GetDate();
            using (var context = new HenXinSMSContext())
            {
                var _username = Request["UserName"];
                var _depart = Request["Depart"];
                var _projectname = Request["ProjectName"];
                var _projectnameid = Request["ProjectNameId"];
                var _projectmanager = Request["ProjectManager"];
                var _contractid = Request["ContractId"];
                var _location = Request["Location"];
                var _company = Request["Company"];
                var _type = Request["Type"];
                var _valueofgoods = Request["ValueofGoods"];
                var _insurance = Request["Insurance"];
                var _insuranceofamount =Convert.ToDouble( Request["InsuranceofAmount"]);
                var _estimatetime_start = Convert.ToDateTime(Request["EstimateTime_Start"]);
                var _estimatetime_end = Convert.ToDateTime(Request["EstimateTime_End"]);
                var _estimatedate_use = Convert.ToDateTime(Request["EstimateDate_Use"]);
                var _estimatedate_completetime = Request["EstimateDate_CompleteTime"];
                var _transportrequest = Request["TransportRequest"];
                var _numoftruck = Request["NumofTruck"];
                var _typeoftransport = Request["TypeofTransport"];
                var _consignee = Request["Consignee"];
                var _cargo = Request["Cargo"];
                var _address = Request["Address"];
                var _phone = Request["Phone"];
                var _fax = Request["Fax"];
                var _remark = Request["Remark"];
                int n = date.days(_estimatedate_use);
                var transport = new Transport
                {
                    UserName = _username,
                    Depart = _depart,
                    ProjectName = _projectname,
                    ProjectNameId = _projectnameid,
                    ProjectManager = _projectmanager,
                    ContractId = _contractid,
                    Location = _location,
                    Company=_company,
                    Type=_type,
                    ValueofGoods = _valueofgoods,
                    Insurance = _insurance,
                    InsuranceofAmount = _insuranceofamount,
                    EstimateTime_Start = _estimatetime_start.AddDays(n),
                    EstimateTime_End = _estimatetime_end.AddDays(n),
                    EstimateDate_Use = _estimatedate_use,
                    EstimateDate_CompleteTime = Convert.ToDateTime(_estimatedate_completetime),
                    TransportRequest = _transportrequest,
                    NumofTruck = _numoftruck,
                    TypeofTransport = _typeoftransport,
                    Remark=_remark,
                    Consignee=_consignee,
                    Cargo=_cargo,
                    Address=_address,
                    Phone=_phone,
                    Fax=_fax,
                 
                };
                context.Transports.Add(transport);
                context.SaveChanges();
                var _transports = context.Transports;
                var _transport = _transports.First(d => d.EstimateDate_Use == _estimatedate_use);
                if (_transport != null)
                {
                    return Content("Success");
                }
                else
                {
                    return Content("Error");
                }
            }
        }
        public ActionResult delete(int id)
        {
            using (var context = new HenXinSMSContext())
            {
                try
                {

                    Transport transportsToDelete = new Transport() { ID = id };
                    context.Entry(transportsToDelete).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    return RedirectToAction("transportlist", new { id = id, saveChangesError = true });
                }
                return RedirectToAction("transportlist");
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
                Transport transport = context.Transports.Find(id);
                if (transport == null)
                {
                    return HttpNotFound();
                }
                return View(transport);
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
                Transport transport = context.Transports.Find(id);
                if (transport == null)
                {
                    return HttpNotFound();
                }
                return View(transport);
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
                Transport transport = context.Transports.Find(id);
                if (transport == null)
                {
                    return HttpNotFound();
                }
                return View(transport);
            }
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult edit(Transport transport)
        {
            using (var context = new HenXinSMSContext())
            {
                if (ModelState.IsValid)
                {
                    context.Entry(transport).State = EntityState.Modified;
                    context.SaveChanges();
                    return RedirectToAction("transportlist");
                }
                return View(transport);
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
                    return RedirectToAction("newtransport", new { id = id, saveChangesError = true });
                }
                return RedirectToAction("newtransport");
            }
        }
        public ActionResult check(int id)
        {
            GetDate date = new GetDate();
            using (var context = new HenXinSMSContext())
            {
                try
                {
                    var transports = context.Transports;
                    if (transports.Any())
                    {
                        var transportToUpdate = transports.Find(id);
                        transportToUpdate.OML_Review = "已审核";
                        transportToUpdate.ApplyId = "T" + date.Display();
                        context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    return RedirectToAction("transportlist", new { id = id, saveChangesError = true });
                }
                return RedirectToAction("successtransport");
            }
        }
        public ActionResult checktransports(string sortOrder, string searchString, string currentFilter, int? page)
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


                var transports = from w in context.Transports
                                 where w.PM_Review == "已审核" && w.OML_Review != "已审核" && w.Status == "Active" && w.Schedule == "Inactive"
                                 select w;
                if (!string.IsNullOrEmpty(searchString))
                {
                    transports = transports.Where(w => w.UserName.Contains(searchString) || w.ProjectName.Contains(searchString)|| w.OML_Review.Contains(searchString) || w.PM_Review.Contains(searchString));

                }
                switch (sortOrder)
                {
                    case "first_desc":
                        transports = transports.OrderByDescending(w => w.ProjectName);
                        break;
                    case "last_desc":
                        transports = transports.OrderByDescending(w => w.ApplicationDate);
                        break;
                    case "last":
                        transports = transports.OrderBy(w => w.ApplicationDate);
                        break;
                    default:
                        transports = transports.OrderBy(w => w.ProjectName);
                        break;
                }
                int pageSize = 6;
                int pageNumber = (page ?? 1);
                return View(transports.ToPagedList(pageNumber, pageSize));

            }
        }
        public ActionResult successtransport(string sortOrder, string searchString, string currentFilter, int? page)
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


                var transports = from w in context.Transports
                                 where w.PM_Review == "已审核"
                                   && w.OML_Review == "已审核"
                                   && w.Schedule == "Inactive"
                                 select w;
                if (!string.IsNullOrEmpty(searchString))
                {
                    transports = transports.Where
                        (w => w.UserName.Contains(searchString) || w.ProjectName.Contains(searchString)  || w.ApplyId.Contains(searchString) || w.ApplicationDate.ToString().Contains(searchString));

                }
                switch (sortOrder)
                {
                    case "first_desc":
                        transports = transports.OrderByDescending(w => w.ProjectName);
                        break;
                    case "last_desc":
                        transports = transports.OrderByDescending(w => w.ApplicationDate);
                        break;
                    case "last":
                        transports = transports.OrderBy(w => w.ApplicationDate);
                        break;
                    default:
                        transports = transports.OrderBy(w => w.ProjectName);
                        break;
                }
                int pageSize = 6;
                int pageNumber = (page ?? 1);
                return View(transports.ToPagedList(pageNumber, pageSize));

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
            var filePath = Server.MapPath(string.Format("~/{0}", "Transports"));
            file.SaveAs(Path.Combine(filePath, fileName));
            return View();
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


                var transports = from w in context.Transports
                                 where
                                 w.Status == "Active" && w.Schedule == "Active"
                                 && w.DateRecorded==w.ApplicationDate
                                 select w;
                if (!string.IsNullOrEmpty(searchString))
                {
                    transports = transports.Where(w => w.UserName.Contains(searchString)
                                               || w.ProjectName.Contains(searchString)
                                               || w.Location.Contains(searchString)
                                               || w.ApplyId.Contains(searchString)
                                               || w.ProjectNameId.Contains(searchString));

                }
                switch (sortOrder)
                {
                    case "first_desc":
                        transports = transports.OrderByDescending(w => w.ProjectName);
                        break;
                    case "last_desc":
                        transports = transports.OrderByDescending(w => w.ApplicationDate);
                        break;
                    case "last":
                        transports = transports.OrderBy(w => w.ApplicationDate);
                        break;
                    default:
                        transports = transports.OrderBy(w => w.ProjectName);
                        break;
                }
                int pageSize = 8;
                int pageNumber = (page ?? 1);
                return View(transports.ToPagedList(pageNumber, pageSize));

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


                var transports = from w in context.Transports
                                 where
                                 w.Status == "Active" && w.Schedule == "Active"
                                 && w.DateRecorded!=w.ApplicationDate
                                 select w;
                if (!string.IsNullOrEmpty(searchString))
                {
                    transports = transports.Where(w => w.UserName.Contains(searchString)
                                               || w.ProjectName.Contains(searchString)
                                               || w.Location.Contains(searchString)
                                               || w.ApplyId.Contains(searchString)
                                               || w.ProjectNameId.Contains(searchString));

                }
                switch (sortOrder)
                {
                    case "first_desc":
                        transports = transports.OrderByDescending(w => w.ProjectName);
                        break;
                    case "last_desc":
                        transports = transports.OrderByDescending(w => w.ApplicationDate);
                        break;
                    case "last":
                        transports = transports.OrderBy(w => w.ApplicationDate);
                        break;
                    default:
                        transports = transports.OrderBy(w => w.ProjectName);
                        break;
                }
                int pageSize = 8;
                int pageNumber = (page ?? 1);
                return View(transports.ToPagedList(pageNumber, pageSize));

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
                Transport transport = context.Transports.Find(id);
                if (transport == null)
                {
                    return HttpNotFound();
                }
                Session["ID_Transport"] = id;
                return View(transport);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult cost(Transport transport)
        {
            using (var context = new HenXinSMSContext())
            {
                if (ModelState.IsValid)
                {
                    context.Entry(transport).State = EntityState.Modified;
                    context.SaveChanges();
                    return RedirectToAction("after_Schedule");
                }
                return View(transport);
            }
        }
        public ActionResult transportlistOfysw(string sortOrder, string searchString, string currentFilter, int? page)
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


                var transports = from w in context.Transports
                                 where w.Status == "Active"
                              
                                && w.OML_Review == "已审核"
                                && w.PM_Review == "已审核"
                                && w.Location == "YSW"
                                 select w;
                if (!string.IsNullOrEmpty(searchString))
                {
                    transports = transports.Where
                      (w => w.UserName.Contains(searchString)
                                               || w.ProjectName.Contains(searchString)
                                               || w.ApplyId.Contains(searchString)
                                               || w.ApplicationDate.ToString().Contains(searchString)
                                               || w.Schedule.Contains(searchString)
                                               || w.ProjectNameId.Contains(searchString));

                }
                switch (sortOrder)
                {
                    case "first_desc":
                        transports = transports.OrderByDescending(w => w.ProjectName);
                        break;
                    case "last_desc":
                        transports = transports.OrderByDescending(w => w.ApplicationDate);
                        break;
                    case "last":
                        transports = transports.OrderBy(w => w.ApplicationDate);
                        break;
                    default:
                        transports = transports.OrderBy(w => w.ProjectName);
                        break;
                }
                int pageSize = 6;
                int pageNumber = (page ?? 1);
                return View(transports.ToPagedList(pageNumber, pageSize));

            }
        }
        public ActionResult transportlistOfywp(string sortOrder, string searchString, string currentFilter, int? page)
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


                var transports = from w in context.Transports
                                 where w.Status == "Active"

                                && w.OML_Review == "已审核"
                                && w.PM_Review == "已审核"
                                && w.Location == "YWP"
                                 select w;
                if (!string.IsNullOrEmpty(searchString))
                {
                    transports = transports.Where
                      (w => w.UserName.Contains(searchString)
                                               || w.ProjectName.Contains(searchString)
                                               || w.ApplyId.Contains(searchString)
                                               || w.ApplicationDate.ToString().Contains(searchString)
                                               || w.Schedule.Contains(searchString)
                                               || w.ProjectNameId.Contains(searchString));

                }
                switch (sortOrder)
                {
                    case "first_desc":
                        transports = transports.OrderByDescending(w => w.ProjectName);
                        break;
                    case "last_desc":
                        transports = transports.OrderByDescending(w => w.ApplicationDate);
                        break;
                    case "last":
                        transports = transports.OrderBy(w => w.ApplicationDate);
                        break;
                    default:
                        transports = transports.OrderBy(w => w.ProjectName);
                        break;
                }
                int pageSize = 6;
                int pageNumber = (page ?? 1);
                return View(transports.ToPagedList(pageNumber, pageSize));

            }
        }
        public ActionResult schedule_transport_ysw(int id)
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
                    return RedirectToAction("transportlistOfysw", new { id = id, saveChangesError = true });
                }
                return RedirectToAction("transportlistOfysw");
            }
        }
        public ActionResult schedule_transport_ywp(int id)
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
                    return RedirectToAction("transportlistOfywp", new { id = id, saveChangesError = true });
                }
                return RedirectToAction("transportlistOfywp");
            }
        }
    }
}
