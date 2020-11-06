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
        public static void Add(string detail, string title, string userId)
        {
            Project project = new Project
            {
                Title = title,
                Detail = detail,
                UserId = userId,
                CreatDate = System.DateTime.Now,
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
        public static void Update(int id, string detail, string title, string userId, bool isComplete)
        {
            Project Project = db.Projects.Find(id);
            Project.Title = title;
            Project.Detail = detail;
            Project.UserId = userId;
            Project.IsCompleted = isComplete;
            db.Entry(Project).State = EntityState.Modified;
            db.SaveChanges();
            db.Dispose();
        }
    }
}