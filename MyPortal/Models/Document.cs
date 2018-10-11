namespace MyPortal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Document
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Document()
        {
            StaffDocuments = new HashSet<StaffDocument>();
        }

        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "URL")]
        public string Url { get; set; }

        [Display(Name = "Uploaded By")]
        public int UploaderId { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Display(Name = "Is General")]
        public bool IsGeneral { get; set; }

        public bool Approved { get; set; }

        public virtual Staff Staff { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StaffDocument> StaffDocuments { get; set; }
    }
}
