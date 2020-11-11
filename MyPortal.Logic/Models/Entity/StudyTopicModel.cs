using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class StudyTopicModel : LookupItemModel
    {
        public Guid CourseId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public virtual CourseModel Course { get; set; }
    }
}
