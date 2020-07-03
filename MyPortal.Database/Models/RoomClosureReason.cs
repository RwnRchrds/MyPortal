using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("RoomClosureReason")]
    public class RoomClosureReason : LookupItem, ISystemEntity
    {
        public RoomClosureReason()
        {
            Closures = new HashSet<RoomClosure>();
        }

        [Column(Order = 3)]
        public bool System { get; set; }

        [Column(Order = 4)]
        public bool Exam { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoomClosure> Closures { get; set; }
    }
}
