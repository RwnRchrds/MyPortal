using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Business
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
