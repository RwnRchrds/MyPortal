namespace MyPortal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Result
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Result Set")]
        public int ResultSet { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Student")]
        public int Student { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Subject")]
        public int Subject { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Result")]
        public string Value { get; set; }

        public virtual ResultSet ResultSet1 { get; set; }

        public virtual Student Student1 { get; set; }

        public virtual Subject Subject1 { get; set; }
    }
}
