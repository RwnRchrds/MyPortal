using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("Agents")]
    public class Agent : BaseTypes.Entity, ISoftDeleteEntity
    {
        public Agent()
        {
            LinkedStudents = new HashSet<StudentAgentRelationship>();
        }

        [Column(Order = 2)] public Guid AgencyId { get; set; }

        [Column(Order = 3)] public Guid PersonId { get; set; }

        [Column(Order = 4)] public Guid AgentTypeId { get; set; }

        [Column(Order = 5)]
        [StringLength(128)]
        public string JobTitle { get; set; }

        [Column(Order = 6)] public bool Deleted { get; set; }

        public virtual Agency Agency { get; set; }
        public virtual AgentType AgentType { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<StudentAgentRelationship> LinkedStudents { get; set; }
    }
}