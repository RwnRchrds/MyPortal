using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("StaffIllnessTypes")]
    public class StaffIllnessType : LookupItem, ISystemEntity
    {
        public StaffIllnessType()
        {
            Absences = new HashSet<StaffAbsence>();
        }

        [Column(Order = 3)]
        public bool System { get; set; }

        public virtual ICollection<StaffAbsence> Absences { get; set; }
    }
}
