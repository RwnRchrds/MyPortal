using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Entity
{
    public class StudyTopicModel
    {
        public Guid Id { get; set; }

        public Guid SubjectId { get; set; }

        public Guid YearGroupId { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public virtual SubjectModel Subject { get; set; }

        public virtual YearGroupModel YearGroup { get; set; }
    }
}
