using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Dtos
{
    public class AcademicYearDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        public DateTime FirstDate { get; set; }
        public DateTime LastDate { get; set; }
    }
}
