using System.ComponentModel;

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

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(3)]
        public string Leader { get; set; }

        //ID Referring to Subject Mapped to Results in 4Matrix --> Used to link subjects between MyPortal and 4Matrix
        [DisplayName("4Matrix Subject ID (KS3)")]
        public int? QsiKs3 { get; set; }
      
        [DisplayName("4Matrix Subject ID (KS4)")]
        public int? QsiKs4 { get; set; }

        //ID Referring to the mapped 4Matrix Qualification for Subject --> Used to cross-reference
        [DisplayName("4Matrix Qualification ID (KS3)")]
        public int? FourMIdKs3 { get; set; }

        [DisplayName("4Matrix Qualification ID (KS4)")]
        public int? FourMIdKs4 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Result> Results { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
