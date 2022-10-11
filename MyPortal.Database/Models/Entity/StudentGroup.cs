using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("StudentGroups")]
    public class StudentGroup : LookupItem, ISystemEntity
    {
        [Column(Order = 3)]
        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        [Column(Order = 5)]
        public Guid? PromoteToGroupId { get; set; }

        [Column(Order = 6)]
        public Guid? MainSupervisorId { get; set; }
        
        [Column(Order = 7)]
        public int? MaxMembers { get; set; }
        
        [Column(Order = 8)]
        [StringLength(256)]
        public string Notes { get; set; }
        
        [Column(Order = 9)]
        public bool System { get; set; }

        public virtual StudentGroup PromoteToGroup { get; set; }
        public virtual StudentGroupSupervisor MainSupervisor { get; set; }
        public virtual ICollection<StudentGroupMembership> StudentMemberships { get; set; }
        public virtual ICollection<StudentGroupSupervisor> StudentGroupSupervisors { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<RegGroup> RegGroups { get; set; }
        public virtual ICollection<YearGroup> YearGroups { get; set; }
        public virtual ICollection<House> Houses { get; set; }
        public virtual ICollection<CurriculumBand> CurriculumBands { get; set; }
        public virtual ICollection<CurriculumGroup> CurriculumGroups { get; set; }
        public virtual ICollection<MarksheetTemplateGroup> MarksheetTemplateGroups { get; set; }
        public virtual ICollection<StudentGroup> PromotionSourceGroups { get; set; }
    }
}