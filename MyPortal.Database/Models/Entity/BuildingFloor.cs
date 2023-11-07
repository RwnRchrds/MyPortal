using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("BuildingFloors")]
    public class BuildingFloor : BaseTypes.LookupItem
    {
        [Column(Order = 4)] public Guid BuildingId { get; set; }

        public virtual Building Building { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}