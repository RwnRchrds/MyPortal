using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("LogNoteType")]
    public partial class LogNoteType : LookupItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LogNoteType()
        {
            LogNotes = new HashSet<LogNote>();
        }
        
        [DataMember]
        [StringLength(128)] 
        public string ColourCode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LogNote> LogNotes { get; set; }
    }
}
