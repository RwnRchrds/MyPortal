using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Database.Models;

namespace MyPortal.Logic.Models.Dtos
{
    public class StudyTopicDto
    {
        public int Id { get; set; }

        public int SubjectId { get; set; }

        public int YearGroupId { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public virtual SubjectDto Subject { get; set; }

        public virtual YearGroupDto YearGroup { get; set; }
    }
}
