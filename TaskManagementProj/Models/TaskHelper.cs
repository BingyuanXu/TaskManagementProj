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
        public static IEnumerable<TSource> DistinctById<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
        public static void Add(string detail,string title,string userId,int projectId, DateTime deadline)
        {
            TaskModel Task = new TaskModel
            {
                Title = title,
                Detail = detail,
                ProjectId = projectId,
                UserId = userId,
                CreatDate = System.DateTime.Now,
                Deadline = deadline,
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
        public static void Update(int taskId,string detail, string title, string userId, int projectId)
        {
            TaskModel UpdatedTask = db.Tasks.Find(taskId);
            UpdatedTask.Title = title;
            UpdatedTask.Detail = detail;
            UpdatedTask.ProjectId = projectId;
            UpdatedTask.UserId = userId;
            db.Entry(UpdatedTask).State = EntityState.Modified;
            db.SaveChanges();
            db.Dispose();
        }

        public static void Finish(int id,string finishedComment)
        {
            TaskModel task = db.Tasks.Find(id);
            if(task.IsCompleted == false)
            {
                task.IsCompleted = true;
                task.FinishedComment = finishedComment;
                Notification notification = new Notification
                {
                    Title = "Task Completed!",
                    Detail = task.Title + " is Completed!",              
                    TaskId = id
                };
                db.Notifications.Add(notification);
                db.SaveChanges();
                db.Dispose();
            }           
        }

        public static void UrgentNote(int id,string detail,string userId)
        {
            TaskModel task = db.Tasks.Include(a => a.Project).Include(a => a.User).FirstOrDefault(a => a.Id == id);
            Notification notification = new Notification
            {
                Title = task.Title + " has a Urgent Note!",
                Detail = detail,
                ProjectId = task.Project.Id

            };
            UrgentNote urgentNote = new UrgentNote
            {
                Detail = detail,
                UserId = userId,
                TaskId = id
            };
            db.Notifications.Add(notification);
            db.UrgentNotes.Add(urgentNote);
            db.SaveChanges();
            db.Dispose();
        }

        public static void OverTime()
        {
            var task = from t in db.Tasks
                             where t.Deadline < DateTime.Now & t.CompletePercentage <100 
                             orderby t.Deadline descending
                             select t;

            Notification notification = new Notification
            {
                Title = "Over Time!",
                Detail = task.ToList()[1].Title + " is over deadline!"
            };
            db.Notifications.Add(notification);
            db.SaveChanges();
            db.Dispose();
        }
    }
}
