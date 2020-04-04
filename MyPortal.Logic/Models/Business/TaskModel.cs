using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Business
{
    public class TaskModel
    {
        public Guid Id { get; set; }
        public Guid AssignedToId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DueDate { get; set; }

        [Required]
        [StringLength(128)]
        public string Title { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        public bool Personal { get; set; }
        public bool Completed { get; set; }
        public virtual HomeworkSubmissionModel HomeworkSubmission { get; set; }
        public virtual PersonModel Person { get; set; }
    }
}
