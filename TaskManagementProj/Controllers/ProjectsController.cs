using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskManagementProj.Models;

namespace TaskManagementProj.Controllers
{
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Projects
        public ActionResult Index()
        {

            var projects = db.Projects.Include(p => p.User);
            var sortedProjects = projects.OrderBy(x => (int)x.Priority).ToList();
            return View(sortedProjects);
        }

        // GET: Projects/Details/5
        [Authorize]
        public ActionResult Details(int? id,bool? isShowAll)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Include(a => a.User).Where(a => a.Id == id).FirstOrDefault();
            if (project == null)
            {
                return HttpNotFound();
            }
            if (isShowAll == false)
            {
                var tasks = from t in db.Tasks
                        .Include(a => a.User)
                        .Include(a => a.Project)
                            where t.ProjectId == id
                            where t.IsCompleted == false
                            orderby t.CompletePercentage descending
                            select t;
                ViewBag.Tasks = tasks;
            }
            else
            {
                var tasks = from t in db.Tasks
                       .Include(a => a.User)
                       .Include(a => a.Project)
                            where t.ProjectId == id                           
                            orderby t.CompletePercentage descending
                            select t;
                ViewBag.Tasks = tasks;
            }
            
            
            return View(project);
        }

        // GET: Projects/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        [Authorize]
        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Detail,IsCompleted,CreatDate,deadline,Priority")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                project.UserId = User.Identity.GetUserId();
                if(UserManager.CheckRoleExist("Project Manager"))
                {
                    UserManager.AddUserToRole(project.UserId, "Project Manager");
                }
                else
                {
                    UserManager.AddNewRole("Project Manager");
                    UserManager.AddUserToRole(project.UserId, "Project Manager");
                }
                UserManager.AddNewRole("Developer");
                project.Priority = Priority.Low;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", project.UserId);
            return View(project);
        }

        // GET: Projects/Edit/5
        [Authorize(Roles = "Project Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", project.UserId);
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Project Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Detail,UserId,IsCompleted,CreatDate,deadline,Priority")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", project.UserId);
            return View(project);
        }

        // GET: Projects/Delete/5
        [Authorize(Roles = "Project Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [Authorize(Roles = "Project Manager")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
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

        [Authorize(Roles = "Project Manager")]
        public ActionResult MarkProjectCompleted(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Project Manager")]
        public ActionResult MarkProjectCompleted(int id)
        {
            if (ModelState.IsValid)
            {
                ProjectHelper.Finish(id);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }


        // [Authorize(Roles = "Project Manager")]
        public ActionResult NotificationList()
        {
            var userId = User.Identity.GetUserId();
            var notifications = db.Notifications
                                .Include(a => a.Project.User)
                                .Include(a => a.Task)
                                .Include(a => a.Project)
                                .Where(n => n.Project.UserId == userId || n.Task.UserId == userId);
            return View(notifications);
        }
    }
}