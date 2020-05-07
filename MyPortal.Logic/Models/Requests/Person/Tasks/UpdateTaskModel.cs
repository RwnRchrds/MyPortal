using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Attributes;

namespace MyPortal.Logic.Models.Requests.Person.Tasks
{
    public class UpdateTaskModel : CreateTaskModel
    {
        [NotEmpty] 
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [DateInFuture]
        public DateTime? DueDate { get; set; }
        
        public bool Completed { get; set; }
    }
}