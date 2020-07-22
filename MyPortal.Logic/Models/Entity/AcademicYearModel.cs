using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Entity
{
    public class AcademicYearModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(128)]
        public string Name { get; set; }

        [Required(ErrorMessage = "First Date is required.")]
        public DateTime FirstDate { get; set; }

        [Required(ErrorMessage = "Last Date is required.")]
        public DateTime LastDate { get; set; }
    }
}
