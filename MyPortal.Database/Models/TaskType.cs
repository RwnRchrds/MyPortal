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

        [DataMember] 
        public bool Personal { get; set; }

        [Required]
        public string ColourCode { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
