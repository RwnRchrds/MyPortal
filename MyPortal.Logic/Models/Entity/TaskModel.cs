using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Models.DataGrid;

namespace MyPortal.Logic.Models.Entity
{
    public class TaskModel
    {
        public Guid Id { get; set; }

        public Guid TypeId { get; set; }

        public Guid AssignedToId { get; set; }

        public Guid? AssignedById { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        [Required]
        [StringLength(128)]
        public string Title { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        public bool Completed { get; set; }
        public virtual HomeworkSubmissionModel HomeworkSubmission { get; set; }
        public virtual PersonModel AssignedTo { get; set; }
        public virtual UserModel AssignedBy { get; set; }
        public virtual TaskTypeModel Type { get; set; }

        #region Business Logic

        public bool Overdue
        {
            get { return !Completed && DueDate <= DateTime.Today; }
        }

        public TaskListModel ToListModel(bool editPersonalOnly)
        {
            return new TaskListModel(this, editPersonalOnly);
        }
        
        #endregion
    }
}
