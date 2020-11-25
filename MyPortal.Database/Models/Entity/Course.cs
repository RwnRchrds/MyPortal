using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models.Entity
{
    [Table("Courses")]
    public class Course : LookupItem
    {
        public Course()
        {
            Awards = new HashSet<ExamAward>();
            Classes = new HashSet<Class>();
            StudyTopics = new HashSet<StudyTopic>();
        }

        [Column(Order = 3)]
        public Guid SubjectId { get; set; }

        [Column(Order = 4)]
        public string Name { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual ICollection<ExamAward> Awards { get; set; }

        public virtual ICollection<Class> Classes { get; set; }

        public virtual ICollection<StudyTopic> StudyTopics { get; set; }
    }
}
