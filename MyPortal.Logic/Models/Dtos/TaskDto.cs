using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Database.Models;

namespace MyPortal.Logic.Models.Dtos
{
    public class TaskDto
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
        public virtual HomeworkSubmissionDto HomeworkSubmission { get; set; }
        public virtual PersonDto Person { get; set; }
    }
}
