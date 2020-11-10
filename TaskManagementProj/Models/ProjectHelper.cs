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

        public static void Finish(int id,int? projectId,int? taskId)
        {
            Project Project = db.Projects.Find(id);
            Project.IsCompleted = true;
            Notification notification = new Notification
            {
                Title = "Project Completed!",
                Detail = Project.Title + " is Completed!",
                ProjectId = projectId,
                TaskId = taskId
            };
            db.Notifications.Add(notification);
            db.SaveChanges();
            db.Dispose();
        }


    }
}