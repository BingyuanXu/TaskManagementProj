using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagementProj.Models;
using Microsoft.AspNet.Identity;
using System.Data;
using System.Data.Entity;
using System.Net;

namespace TaskManagementProj.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
           ProjectHelper.ProjectOvertimeWithUnfinishedTasks();
            var userId = User.Identity.GetUserId();
            var notification = from n in db.Notifications
                               where n.IsRead == false
                               select n;
            var notificationNumberForUser = db.Notifications
                                .Include(a => a.Project.User)
                                .Include(a => a.Task)
                                .Include(a => a.Project)
                                .Where(n => n.Project.UserId == userId || n.Task.UserId == userId);
            ViewBag.NotificationNumber = notificationNumberForUser.Count();
            ViewBag.NotificationNumberForUser = notificationNumberForUser.Count().ToString();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}