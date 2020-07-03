using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Attributes;

namespace MyPortal.Logic.Models.Requests.Person.Tasks
{
    public class UpdateTaskModel : CreateTaskModel
    {
        [NotEmpty] 
        public Guid Id { get; set; }

        public new DateTime? DueDate { get; set; }

        public bool? Completed { get; set; }
    }
}