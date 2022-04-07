using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Requests.Behaviour.Incidents;

public class AddToIncidentModel
{
    public Guid IncidentId { get; set; }
    
    public Guid AcademicYearId { get; set; }

    public Guid StudentId { get; set; }

    public Guid? OutcomeId { get; set; }

    public Guid RoleTypeId { get; set; }

    [Required(ErrorMessage = "Status is required.")]
    public Guid StatusId { get; set; }

    [Required(ErrorMessage = "Points is required.")]
    [Range(0, int.MaxValue, ErrorMessage = "Points cannot be negative.")]
    public int Points { get; set; }
}