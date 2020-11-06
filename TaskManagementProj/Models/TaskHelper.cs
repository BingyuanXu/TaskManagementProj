using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskManagementProj.Models;

namespace TaskManagementProj.Models
{
    public static class TaskHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        public static void Add(string detail,string title,string userId,int projectId)
        {
            TaskModel Task = new TaskModel
            {
                Title = title,
                Detail = detail,
                ProjectId = projectId,
                UserId = userId,
                CreatDate = System.DateTime.Now,
                IsCompleted = false
            };
            db.Tasks.Add(Task);
            db.SaveChanges();
            db.Dispose();
        }
        public static void Delete(int id)
        {
            TaskModel task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
            db.SaveChanges();
            db.Dispose();
        }
        public static void Update(int taskId,string detail, string title, string userId, int projectId, bool isComplete)
        {
            TaskModel UpdatedTask = db.Tasks.Find(taskId);
            UpdatedTask.Title = title;
            UpdatedTask.Detail = detail;
            UpdatedTask.ProjectId = projectId;
            UpdatedTask.UserId = userId;
            UpdatedTask.IsCompleted = isComplete;
            db.Entry(UpdatedTask).State = EntityState.Modified;
            db.SaveChanges();
            db.Dispose();
        }
    }
}
