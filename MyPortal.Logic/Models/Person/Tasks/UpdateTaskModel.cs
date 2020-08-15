using System;
using MyPortal.Logic.Attributes;

namespace MyPortal.Logic.Models.Person.Tasks
{
    public class UpdateTaskModel : CreateTaskModel
    {
        [NotEmpty] 
        public Guid Id { get; set; }

        public new DateTime? DueDate { get; set; }

        public bool? Completed { get; set; }
    }
}