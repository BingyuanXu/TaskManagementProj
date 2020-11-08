using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagementProj.Models;

namespace TaskManagementProj.Controllers
{
    [Authorize]
    [Authorize(Roles = "User Manager")]
    public class UserController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllUserNames()
        {
            ViewBag.Users = UserManager.GetAllUserNames();
            return View();
        }

        public ActionResult GetAllRoles()
        {
            var roles = UserManager.GetAllRoles();
            return View(roles);
        }

        public ActionResult AddNewRole()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewRole(string roleName)
        {
            UserManager.AddNewRole(roleName);
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("index");
        }

        public ActionResult AddUserToRole(string userId)
        {
            ViewBag.UserName = db.Users.Find(userId).UserName;
            ViewBag.roleName = new SelectList(db.Roles, "Name", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUserToRole(string roleName, string userId)
        {
            var r = UserManager.AddUserToRole(userId, roleName);
            if (r.Succeeded)
            {
                db.SaveChanges();
            }

            ViewBag.UserName = db.Users.Find(userId).UserName;
            ViewBag.roleName = new SelectList(db.Roles, "Name", "Name");
            db.Dispose();
            return RedirectToAction("index", new { userId });
        }
    }
}