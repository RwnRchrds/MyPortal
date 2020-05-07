using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Models
{
    [Table("Task")]
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid AssignedToId { get; set; }
        public Guid? AssignedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DueDate { get; set; }

        [Required]
        [StringLength(128)]
        public string Title { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        public bool Personal { get; set; }
        public bool Completed { get; set; }
        public virtual HomeworkSubmission HomeworkSubmission { get; set; }
        public virtual Person AssignedTo { get; set; }
        public virtual ApplicationUser AssignedBy { get; set; }
    }
}
