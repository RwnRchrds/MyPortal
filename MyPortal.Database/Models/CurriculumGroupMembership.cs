using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("CurriculumGroupMemberships")]
    public class CurriculumGroupMembership : Entity
    {
        [Column(Order = 1)]
        public Guid StudentId { get; set; }
        
        [Column(Order = 2)]
        public Guid GroupId { get; set; }
        
        [Column(Order = 3)]
        public DateTime StartDate { get; set; }
        
        [Column(Order = 4)]
        public DateTime EndDate { get; set; }

        public virtual Student Student { get; set; }
        public virtual CurriculumGroup CurriculumGroup { get; set; }
    }
}