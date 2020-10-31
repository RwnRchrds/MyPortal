using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ClassModel : BaseModel
    {
        public Guid CourseId { get; set; }

        public Guid GroupId { get; set; }

        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        public virtual CourseModel Course { get; set; }
        public virtual CurriculumGroupModel Group { get; set; }
    }
}
