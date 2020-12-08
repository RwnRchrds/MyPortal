using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Requests.Behaviour.Incidents
{
    public class CreateIncidentModel
    {
        public Guid AcademicYearId { get; set; }

        [Required(ErrorMessage = "Behaviour type is required.")]
        public Guid BehaviourTypeId { get; set; }

        public Guid StudentId { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        public Guid LocationId { get; set; }

        public Guid? OutcomeId { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public Guid StatusId { get; set; }

        public string Comments { get; set; }

        [Required(ErrorMessage = "Points is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Points cannot be negative.")]
        public int Points { get; set; }
    }
}
