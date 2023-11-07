using System.Collections.Generic;
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

        [Column(Order = 4)] public bool System { get; set; }


        public virtual ICollection<RoomClosure> Closures { get; set; }
    }
}