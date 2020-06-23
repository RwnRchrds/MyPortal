using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("TaskType")]
    public class TaskType : LookupItem
    {
        public TaskType()
        {
            Tasks = new HashSet<Task>();
        }

        [Column(Order = 3)] 
        public bool Personal { get; set; }

        [Column(Order = 4)]
        [Required]
        public string ColourCode { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
