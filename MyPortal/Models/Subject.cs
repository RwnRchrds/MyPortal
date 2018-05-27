namespace MyPortal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Subject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Subject()
        {
            Results = new HashSet<Result>();
        }

        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(3)]
        [Display(Name = "Head of Department")]
        public string Leader { get; set; }

        [Display(Name = "4Matrix Qualification ID (KS3)")]
        public int? QsiKs3 { get; set; }

        [Display(Name = "4Matrix Qualifcation ID (KS4)")]
        public int? QsiKs4 { get; set; }

        [Display(Name = "4Matrix Subject ID (KS3)")]
        public int? FourMIdKs3 { get; set; }

        [Display(Name = "4Matrix Subject ID (KS4)")]
        public int? FourMIdKs4 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Result> Results { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
