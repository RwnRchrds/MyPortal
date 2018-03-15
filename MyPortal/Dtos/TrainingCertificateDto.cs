using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using MyPortal.Models;

namespace MyPortal.Dtos
{
    public class TrainingCertificateDto
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Course { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string Staff { get; set; }

        [Required]
        public int Status { get; set; }

        public TrainingCourse TrainingCourse { get; set; }
    }
}