using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity;

[Table("CurriculumGroupSessions")]
public class CurriculumGroupSession : BaseTypes.Entity
{
    [Column(Order = 2)] public Guid CurriculumGroupId { get; set; }

    [Column(Order = 3)] public Guid SubjectId { get; set; }

    [Column(Order = 4)] public Guid SessionTypeId { get; set; }

    [Column(Order = 5)] public int SessionAmount { get; set; }

    public virtual CurriculumGroup CurriculumGroup { get; set; }
    public virtual Subject Subject { get; set; }
    public virtual SessionType SessionType { get; set; }
}