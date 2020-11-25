using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models.Entity
{
    [Table("SubjectCodes")]
    public class SubjectCode : LookupItem
    {
        public SubjectCode()
        {
            Elements = new HashSet<ExamBaseElement>();
        }

        [Column(Order = 3)]
        public string Code { get; set; }

        [Column(Order = 4)] 
        public Guid SubjectCodeSetId { get; set; }

        public virtual SubjectCodeSet SubjectCodeSet { get; set; }
        public virtual ICollection<ExamBaseElement> Elements { get; set; }
    }
}
