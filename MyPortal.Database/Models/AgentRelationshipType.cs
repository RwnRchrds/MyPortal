using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("AgentRelationshipTypes")]
    public class AgentRelationshipType : LookupItem
    {
        // TODO: Populate Data

        public virtual ICollection<StudentAgentRelationship> Relationships { get; set; }
    }
}
