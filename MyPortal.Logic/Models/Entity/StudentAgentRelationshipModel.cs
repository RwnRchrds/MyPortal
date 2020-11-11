using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class StudentAgentRelationshipModel : BaseModel
    {
        public Guid StudentId { get; set; }
        
        public Guid AgentId { get; set; }
        
        public Guid RelationshipTypeId { get; set; }

        public virtual StudentModel Student { get; set; }
        public virtual AgentModel Agent { get; set; }
        public virtual AgentRelationshipTypeModel RelationshipType { get; set; }
    }
}