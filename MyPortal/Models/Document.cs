using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MyPortal.Models
{
    public class Document
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Document()
        {
            StaffDocuments = new HashSet<StaffDocument>();
            StudentDocuments = new HashSet<StudentDocument>();
        }

        public int Id { get; set; }

        [Required] [StringLength(255)] public string Description { get; set; }

        [Required] [StringLength(255)] public string Url { get; set; }

        public bool IsGeneral { get; set; }

        [Column(TypeName = "date")] public DateTime Date { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StaffDocument> StaffDocuments { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentDocument> StudentDocuments { get; set; }
    }
}