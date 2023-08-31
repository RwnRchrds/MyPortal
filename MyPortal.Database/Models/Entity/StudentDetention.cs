using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity;

[Table("StudentDetentions")]
public class StudentDetention : BaseTypes.Entity
{
    [Column(Order = 2)]
    public Guid StudentId { get; set; }

    [Column(Order = 3)]
    public Guid DetentionId { get; set; }
    
    [Column(Order = 4)]
    public Guid? LinkedIncidentId { get; set; }

    [Column(Order = 4)]
    public bool Attended { get; set; }

    [Column(Order = 5)]
    public string Notes { get; set; }

    public virtual Student Student { get; set; }
    public virtual Detention Detention { get; set; }
    public virtual StudentIncident LinkedIncident { get; set; }
}