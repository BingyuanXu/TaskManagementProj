using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TaskManagementProj.Models;

namespace TaskManagementProj.Controllers
{
    public class TasksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize(Roles = "Developer")]
        public ActionResult MarkComplete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskModel taskModel = db.Tasks.Find(id);
            if (taskModel == null)
            {
                return HttpNotFound();
            }
            return View(taskModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Developer")]
        public ActionResult MarkComplete(int id, string finishedComment)
        {                    
            if (ModelState.IsValid)
            {
                TaskHelper.Finish(id, finishedComment);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");           
        }
        // GET: Tasks
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            bool isManager = UserManager.CheckUserInRole(userId, "Project Manager");
            
            if (isManager)
            {
                var taskModels = from a in db.Tasks
                                   .Include(t => t.Project)
                                   .Include(t => t.User)
                                 select a;
                return View(taskModels.ToList());
            }
            else
            {
                var taskModels = from a in db.Tasks
                                .Include(t => t.Project)
                                .Include(t => t.User)
                                 where a.UserId == userId
                                 select a;
                return View(taskModels.ToList());
            }
        }

        // GET: Tasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TaskModel taskModel = db.Tasks.Include(a => a.User)
                                          .Include(a => a.Project)
                                          .Where(t => t.Id == id)
                                          .FirstOrDefault();
            if (taskModel == null)

            {
                return HttpNotFound();
            }
            return View(taskModel);
        }

        // GET: Tasks/Create
      //  [Authorize(Roles = "Manager")]    use later
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Title");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Tasks/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性；有关
        // 更多详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string detail, string title, string userId, int projectId, DateTime deadline)
        {      
            if (ModelState.IsValid)
            {
                TaskHelper.Add(detail, title, userId, projectId, deadline);
                UserManager.AddUserToRole(userId, "Developer");
                return Redirect("../Projects");
            }
            return Redirect("../Projects");
        }

        // GET: Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskModel taskModel = db.Tasks.Find(id);
            if (taskModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Title", taskModel.ProjectId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", taskModel.UserId);
            return View(taskModel);
        }

        // POST: Tasks/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性；有关
        // 更多详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int taskId, string detail, string title, string userId, int projectId)
        {
            if (ModelState.IsValid)
            {
                TaskHelper.Update(taskId, detail, title, userId, projectId);               
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskModel taskModel = db.Tasks.Find(id);
            if (taskModel == null)
            {
                return HttpNotFound();
            }
            return View(taskModel);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaskHelper.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult TaskOverDeadline()
        {
            var TaskOverDeadline = db.Tasks.Include(a => a.User)
                                           .Include(a => a.Project)
                                           .Include(a=> a.User)
                                           .Where(t => t.Deadline < DateTime.Now & t.CompletePercentage < 100 & t.IsCompleted == false);                   
            return View(TaskOverDeadline);
        }
    }
}
