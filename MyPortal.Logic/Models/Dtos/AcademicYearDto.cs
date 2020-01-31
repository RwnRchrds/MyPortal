using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Logic.Attributes;

namespace MyPortal.Logic.Models.Dtos
{
    public class AcademicYearDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public DateTime FirstDate { get; set; }

        [AcademicYearLastDate]
        public DateTime LastDate { get; set; }
    }
}
