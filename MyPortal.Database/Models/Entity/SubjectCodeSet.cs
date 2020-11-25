using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models.Entity
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
