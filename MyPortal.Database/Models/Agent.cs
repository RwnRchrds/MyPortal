using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("Agents")]
    public class Agent : Entity, ISoftDeleteEntity
    {
        public Agent()
        {
            LinkedStudents = new HashSet<StudentAgentRelationship>();
        }

        [Column(Order = 1)]
        public Guid AgencyId { get; set; }

        [Column(Order = 2)]
        public Guid PersonId { get; set; }

        [Column(Order = 4)]
        [StringLength(128)]
        public string JobTitle { get; set; }

        [Column(Order = 5)]
        public bool Deleted { get; set; }

        public virtual Agency Agency { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<StudentAgentRelationship> LinkedStudents { get; set; }
    }
}
