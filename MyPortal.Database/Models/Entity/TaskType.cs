using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("TaskTypes")]
    public class TaskType : LookupItem, ISystemEntity, IReservable
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

        [Column(Order = 5)] 
        public bool System { get; set; }

        [Column(Order = 6)] 
        public bool Reserved { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
