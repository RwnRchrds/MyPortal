﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("RoomClosureReasons")]
    public class RoomClosureReason : LookupItem, ISystemEntity
    {
        public RoomClosureReason()
        {
            Closures = new HashSet<RoomClosure>();
        }

        [Column(Order = 4)]
        public bool System { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoomClosure> Closures { get; set; }
    }
}
