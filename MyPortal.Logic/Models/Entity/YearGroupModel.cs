using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Logic.Models.Entity
{
    public class YearGroupModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public Guid? HeadId { get; set; }
        public Guid CurriculumYearGroupId { get; set; }

        public virtual StaffMemberModel HeadOfYear { get; set; }
        public virtual CurriculumYearGroup CurriculumYearGroup { get; set; }
    }
}