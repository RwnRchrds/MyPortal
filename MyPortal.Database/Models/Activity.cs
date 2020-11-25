using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("Activities")]
    public class Activity : Entity, ISoftDeleteEntity
    {
        public Activity()
        {
            Memberships = new HashSet<ActivityMembership>();
        }

        [Column(Order = 1)]
        [StringLength(128)]
        public string Name { get; set; }

        [Column(Order = 2)]
        [StringLength(256)]
        public string Description { get; set; }

        [Column(Order = 3)]
        public DateTime DateStarted { get; set; }

        [Column(Order = 4)]
        public DateTime? DateEnded { get; set; }

        [Column(Order = 5)]
        public int MaxMembers { get; set; }

        [Column(Order = 6)]
        public bool Deleted { get; set; }

        public virtual ICollection<ActivityMembership> Memberships { get; set; }
        public virtual ICollection<ActivitySupervisor> Supervisors { get; set; }
        public virtual ICollection<ActivityEvent> Events { get; set; }
    }
}
