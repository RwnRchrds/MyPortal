using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("EnrolmentStatus")]
    public class EnrolmentStatus : LookupItem, ICensusEntity
    {
        public EnrolmentStatus()
        {
            Students = new HashSet<Student>();
        }

        [Column(Order = 3)]
        public string Code { get; set; }

        public virtual ICollection<Student> Students { get; set; }  
    }
}
