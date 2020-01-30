using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyPortal.Database.Models
{
    [Table("Task")]
    public class Task
    {
        public int Id { get; set; }
        public int AssignedToId { get; set; }
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
        public virtual Person Person { get; set; }
    }
}
