using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models.Entity;

[Table("SessionTypes")]
public class SessionType : LookupItem
{
    [Column(Order = 4)]
    public string Code { get; set; }
    
    [Column(Order = 5)]
    public int Length { get; set; }
    
    public virtual ICollection<CurriculumGroupSession> CurriculumGroupSessions { get; set; }
}