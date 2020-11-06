using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementProj.Models
{
    public class UrgentNote
    {
        public int Id { get; set; }
        public string Detail { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int TaskId { get; set; }
        public TaskModel Task { get; set; } 
    }
}