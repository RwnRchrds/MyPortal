using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("Courses")]
    public class Course : Entity
    {
        public Course()
        {
            Classes = new HashSet<Class>();
        }

        [Column(Order = 1)]
        public Guid SubjectId { get; set; }

        [Column(Order = 2)]
        public Guid LevelId { get; set; }

        [Column(Order = 3)] 
        public Guid? AwardId { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual ExamQualificationLevel Level { get; set; }

        public virtual ExamAward Award { get; set; }

        public virtual ICollection<Class> Classes { get; set; }
    }
}
