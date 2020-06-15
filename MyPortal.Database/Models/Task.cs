using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Models
{
    [Table("Task")]
    public class Task
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid TypeId { get; set; }

        [DataMember]
        public Guid AssignedToId { get; set; }

        [DataMember]
        public Guid? AssignedById { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public DateTime? DueDate { get; set; }

        [DataMember]
        public DateTime? CompletedDate { get; set; }

        [DataMember]
        [Required]
        [StringLength(128)]
        public string Title { get; set; }

        [DataMember]
        [StringLength(256)]
        public string Description { get; set; }

        [DataMember]
        public bool Personal { get; set; }

        [DataMember]
        public bool Completed { get; set; }
        public virtual HomeworkSubmission HomeworkSubmission { get; set; }
        public virtual Person AssignedTo { get; set; }
        public virtual ApplicationUser AssignedBy { get; set; }
        public virtual TaskType Type { get; set; }
    }
}
