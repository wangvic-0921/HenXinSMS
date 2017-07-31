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
    public class TransportList_GuestController : Controller
    {
        //
        // GET: /TransportList_Guest/

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
                int pageSize = 6;
                int pageNumber = (page ?? 1);
                return View(transports.ToPagedList(pageNumber, pageSize));

            }
        }
    }
}
