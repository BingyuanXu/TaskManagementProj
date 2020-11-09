using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementProj.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public decimal Budget { get; set; }
        public bool IsCompleted { get; set; }
        public ICollection<TaskModel> Tasks { get; set; }
        public DateTime CreatDate { get; set; }
        public Project()
        {
            CreatDate = System.DateTime.Now;
            IsCompleted = false;
        }          
    }
}