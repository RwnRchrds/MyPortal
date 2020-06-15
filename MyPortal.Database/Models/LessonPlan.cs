using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Models
{
    [Table("LessonPlan")]
    public partial class LessonPlan : IDirectoryEntity

    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid StudyTopicId { get; set; }

        [DataMember]
        public Guid AuthorId { get; set; }

        [DataMember]
        public Guid DirectoryId { get; set; }

        [DataMember]
        [Required] [StringLength(256)] public string Title { get; set; }

        [DataMember]
        [Required] public string LearningObjectives { get; set; }

        [DataMember]
        [Required] public string PlanContent { get; set; }

        [DataMember]
        [Required] public string Homework { get; set; }

        public virtual Directory Directory { get; set; }
        public virtual ApplicationUser Author { get; set; }
        public virtual StudyTopic StudyTopic { get; set; }
    }
}
