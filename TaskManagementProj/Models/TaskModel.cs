using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManagementProj.Models
{
    public class TaskModel
    {       
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public string FinishedComment { get; set; }
        [Range(0, 100)]
        public int CompletePercentage { get; set; }
        public bool IsCompleted { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public  ICollection<UrgentNote> UrgentNotes { get; set; }
        public DateTime CreatDate { get; set; }
        public DateTime Deadline { get; set; }
        public TaskModel()
        {
            IsCompleted = false;
            CreatDate = System.DateTime.Now;
        }
    }
}