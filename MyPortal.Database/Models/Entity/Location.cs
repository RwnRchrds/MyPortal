﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("Locations")]
    public class Location : BaseTypes.Entity, ISystemEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Location()
        {
            BehaviourAchievements = new HashSet<Achievement>();
            BehaviourIncidents = new HashSet<Incident>();
            Rooms = new HashSet<Room>();
        }

        [Column(Order = 2)]
        [Required]
        [StringLength(128)]
        public string Description { get; set; }

        [Column(Order = 3)] public bool System { get; set; }


        public virtual ICollection<Achievement> BehaviourAchievements { get; set; }


        public virtual ICollection<Incident> BehaviourIncidents { get; set; }


        public virtual ICollection<Room> Rooms { get; set; }
    }
}