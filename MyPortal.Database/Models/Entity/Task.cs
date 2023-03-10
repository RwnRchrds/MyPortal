using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("Tasks")]
    public class Task : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid TypeId { get; set; }

        [Column(Order = 2)]
        public Guid AssignedToId { get; set; }

        [Column(Order = 3)]
        public Guid? AssignedById { get; set; }

        [Column(Order = 4)]
        public DateTime CreatedDate { get; set; }

        [Column(Order = 5)]
        public DateTime? DueDate { get; set; }

        [Column(Order = 6)]
        public DateTime? CompletedDate { get; set; }

        [Column(Order = 7)]
        [Required]
        [StringLength(128)]
        public string Title { get; set; }

        [Column(Order = 8)]
        [StringLength(256)]
        public string Description { get; set; }

        [Column(Order = 9)]
        public bool Completed { get; set; }
        public virtual Person AssignedTo { get; set; }
        public virtual User AssignedBy { get; set; }
        public virtual TaskType Type { get; set; }
        
        public virtual ICollection<HomeworkSubmission> HomeworkSubmissions { get; set; }
    }
}
