﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("StaffIllnessType")]
    public class StaffIllnessType : LookupItem
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