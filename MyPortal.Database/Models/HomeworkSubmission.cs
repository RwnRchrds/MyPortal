using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace MyPortal.Database.Models
{
    [Table("HomeworkSubmission")]
    public class HomeworkSubmission
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid HomeworkId { get; set; }

        [DataMember]
        public Guid StudentId { get; set; }

        [DataMember]
        public Guid TaskId { get; set; }

        [DataMember]
        public int MaxPoints { get; set; }

        [DataMember]
        public int PointsAchieved { get; set; }

        [DataMember]
        public string Comments { get; set; }

        public virtual Homework Homework { get; set; }
        public virtual Student Student { get; set; }
        public virtual Task Task { get; set; }
    }
}
