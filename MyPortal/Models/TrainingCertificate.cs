namespace MyPortal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TrainingCertificate
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Course { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string Staff { get; set; }

        public int? Status { get; set; }

        public virtual Staff Staff1 { get; set; }

        public virtual TrainingCourse TrainingCourse { get; set; }

        public virtual TrainingStatus TrainingStatus { get; set; }
    }
}
