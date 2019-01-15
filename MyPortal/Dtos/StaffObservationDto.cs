using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Dtos
{
    public class StaffObservationDto
    {
        public int Id { get; set; }

        [Required] public DateTime Date { get; set; }

        [Required] public int ObserveeId { get; set; }

        [Required] public int ObserverId { get; set; }

        [Required] public string Outcome { get; set; }

        public StaffDto Staff { get; set; }

        public StaffDto Staff1 { get; set; }
    }
}