using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("StaffAbsenceTypes")]
    public class StaffAbsenceType : LookupItem, ISystemEntity
    {
        public StaffAbsenceType()
        {
            Absences = new HashSet<StaffAbsence>();
        }

        [Column(Order = 3)]
        public bool System { get; set; }

        [Column(Order = 4)]
        public bool Illness { get; set; }

        public virtual ICollection<StaffAbsence> Absences { get; set; }
    }
}
