using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

using System.Linq;
using System.Web;

namespace MyPortal.Models
{
    public partial class Grade
    {
        public int Id { get; set; }

        public int GradeSetId { get; set; }

        [Column("Grade")]
        [Required]
        [StringLength(255)]
        public string GradeValue { get; set; }

        public virtual GradeSet GradeSet { get; set; }
    }
}