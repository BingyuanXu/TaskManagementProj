using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TaskManagementProj.Models
{
    public class ProjectHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        public static void Add(string detail, string title, string userId,DateTime deadline)
        {
            Project project = new Project
            {
                Title = title,
                Detail = detail,
                UserId = userId,
                CreatDate = System.DateTime.Now,
                Deadline = deadline,
                IsCompleted = false
            };
            db.Projects.Add(project);
            db.SaveChanges();
            db.Dispose();
        }
        public static void Delete(int id)
        {
            Project Project = db.Projects.Find(id);
            db.Projects.Remove(Project);
            db.SaveChanges();
            db.Dispose();
        }
        public static void Update(int id, string detail, string title, string userId)
        {
            Project Project = db.Projects.Find(id);
            Project.Title = title;
            Project.Detail = detail;
            Project.UserId = userId;
            db.Entry(Project).State = EntityState.Modified;
            db.SaveChanges();
            db.Dispose();
        }

        public static void Finish(int id)
        {
            Project Project = db.Projects.Find(id);
            Project.IsCompleted = true;
            Notification notification = new Notification
            {
                Title = "Project Completed!",
                Detail = Project.Title + " is Completed!",
                ProjectId = id
            };
            db.Notifications.Add(notification);
            db.SaveChanges();
            db.Dispose();
        }

        public static void ProjectOvertimeWithUnfinishedTasks()
        {
            DateTime CurrentTime = DateTime.Now;
            var projects = from p in db.Projects
                           where p.Deadline < CurrentTime
                           select p;

            var projectsWithUnfinishedTasks = projects.Include(p => p.Tasks)
                                                      .Where(t => t.IsCompleted == false);

            foreach (var p in projectsWithUnfinishedTasks)
            {
                var notifications = from n in db.Notifications
                                    where n.Title == "Project Overtime!"
                                    select n;
                bool notificationExist = false;
                foreach (var n in notifications)
                {
                    if (n.ProjectId == p.Id)
                    {
                        notificationExist = true;
                    }
                }
                //one project can only have one overtime notification
                if (notificationExist == false)
                {
                    Notification notification = new Notification
                    {
                        Title = "Project Overtime!",
                        Detail = p.Title + " has unfinished tasks",
                        ProjectId = p.Id
                    };
                    db.Notifications.Add(notification);
                }
            }
            db.SaveChanges();
            db.Dispose();
        }


    }
}