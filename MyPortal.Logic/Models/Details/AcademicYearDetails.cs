using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Details
{
    public class AcademicYearDetails
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public DateTime FirstDate { get; set; }

        public DateTime LastDate { get; set; }
    }
}
