using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models.Entity
{
    [Table("StudyTopics")]
    public class StudyTopic : LookupItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StudyTopic()
        {
            LessonPlans = new HashSet<LessonPlan>();
        }

        [Column(Order = 4)]
        public Guid CourseId { get; set; }

        [Column(Order = 5)]
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        
        [Column(Order = 6)] 
        public int Order { get; set; }

        public virtual Course Course { get; set; }

        
        public virtual ICollection<LessonPlan> LessonPlans { get; set; }
    }
}
