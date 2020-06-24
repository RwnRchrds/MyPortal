using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("StaffAbsenceType")]
    public class StaffAbsenceType : LookupItem
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
