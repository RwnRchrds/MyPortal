using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models.Entity
{
    [Table("AgentRelationshipTypes")]
    public class AgentRelationshipType : LookupItem
    {
        public virtual ICollection<StudentAgentRelationship> Relationships { get; set; }
    }
}
