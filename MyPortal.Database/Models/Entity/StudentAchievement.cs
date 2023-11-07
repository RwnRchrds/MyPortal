using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity;

[Table("StudentAchievements")]
public class StudentAchievement : BaseTypes.Entity
{
    [Column(Order = 2)] public Guid StudentId { get; set; }

    [Column(Order = 3)] public Guid AchievementId { get; set; }

    [Column(Order = 4)] public Guid? OutcomeId { get; set; }

    [Column(Order = 5)]
    [Range(0, int.MaxValue, ErrorMessage = "Achievement cannot have negative points.")]
    public int Points { get; set; }

    public virtual Student Student { get; set; }
    public virtual Achievement Achievement { get; set; }
    public virtual AchievementOutcome Outcome { get; set; }
}