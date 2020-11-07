using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskManagementProj.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        [ForeignKey("Project")]
        public int? ProjectId { get; set; }
        public virtual Project Project { get; set; }
        [ForeignKey("Task")]
        public int? TaskId { get; set; }
        public virtual TaskModel Task { get; set; }
    }
}