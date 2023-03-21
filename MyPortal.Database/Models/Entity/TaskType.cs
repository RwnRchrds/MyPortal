using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("TaskTypes")]
    public class TaskType : LookupItem, ISystemEntity
    {
        public TaskType()
        {
            Tasks = new HashSet<Task>();
        }

        [Column(Order = 4)] 
        public bool Personal { get; set; }

        [Column(Order = 5)]
        [Required]
        public string ColourCode { get; set; }

        [Column(Order = 6)] 
        public bool System { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
