using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("SubjectCodeSets")]
    public class SubjectCodeSet : LookupItem
    {
        public SubjectCodeSet()
        {
            SubjectCodes = new HashSet<SubjectCode>();
        }

        public virtual ICollection<SubjectCode> SubjectCodes { get; set; }
    }
}
