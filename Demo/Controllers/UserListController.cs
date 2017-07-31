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
    public class UserListController : Controller
    {
        public ActionResult userlist(string sortOrder, string searchString, string currentFilter, int? page)
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
                var users = from w in context.Users
                            select w;
                if (!string.IsNullOrEmpty(searchString))
                {
                    users = users.Where(w => w.UserName.Contains(searchString) || w.Depart.Contains(searchString) || w.Level.Contains(searchString));
                   
                }
                switch (sortOrder)
                {
                    case "first_desc":
                        users = users.OrderByDescending(w => w.UserName);
                        break;
                    case "last_desc":
                        users = users.OrderByDescending(w => w.Depart);
                        break;
                    case "last":
                        users = users.OrderBy(w => w.Depart);
                        break;
                    default:
                        users = users.OrderBy(w => w.UserName);
                        break;
                }
                int pageSize = 7;
                int pageNumber = (page ?? 1);
                return View(users.ToPagedList(pageNumber, pageSize));
             }
        }
        // GET: /UserList/
        public ActionResult newuser()
        {
           
            return View();
        }
        [HttpPost]
         public ActionResult adduser()
        {
           // UserService adduer = new UserService();
       
            CheckRepeat c=new CheckRepeat();
           
                using (var context = new HenXinSMSContext())
                {
                    string _username = Request["UserName"];
                    string _password = Request["PassWord"];
                    string _depart = Request["Depart"];
                    string _level = Request["Level"];
                    int tag = c.search(_username);
                    if (tag==0)
                    {
                        var user = new User { UserName = _username, PassWord = _password, Depart = _depart, Level = _level };
                        context.Users.Add(user);
                        context.SaveChanges();
                        int  x = c.search(_username);
                        if (x==1 )
                        {
                            return Content("Success");
                        }
                        else
                        {
                            return Content("Error");
                        }
                    }
                    else
                    {
                        return Content("Repeat");
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
                User user = context.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
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
                User user = context.Users.Find(id);
                    if (user == null)
                {
                    return HttpNotFound();
                }
                    return View(user);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserName, PassWord, Depart, Level")] User user)
        {
            using (var context = new HenXinSMSContext())
            {
                if (ModelState.IsValid)
                {
                    context.Entry(user).State = EntityState.Modified;
                    context.SaveChanges();
                    return RedirectToAction("userlist");
                }
                return View(user);
            }
        }
        public ActionResult delete(int id)
        {
            using (var context = new HenXinSMSContext())
            {
                try
                {
                    User workerToDelete = new User() { ID = id };
                    context.Entry(workerToDelete).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    return RedirectToAction("userlist", new { id = id, saveChangesError = true });
                }
                return RedirectToAction("userlist");
            }
        }
        public ActionResult userinfo()
        {
            string _username=Session["UserName"].ToString();
            using (var context = new HenXinSMSContext())
            {
                User user = context.Users.First(d => d.UserName == _username);

                return View(user);
            }
            //return View();
        }
    }
}
