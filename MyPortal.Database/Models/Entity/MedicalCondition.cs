using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models.Entity
{
    [Table("MedicalConditions")]
    public class MedicalCondition : LookupItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MedicalCondition()
        {
            PersonConditions = new HashSet<PersonCondition>();  
        }

        public virtual ICollection<PersonCondition> PersonConditions { get; set; }
    }
}