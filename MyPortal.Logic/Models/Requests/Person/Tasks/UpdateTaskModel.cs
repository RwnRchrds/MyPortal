using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Attributes;

namespace MyPortal.Logic.Models.Requests.Person.Tasks
{
    public class UpdateTaskModel
    {
        [NotEmpty] 
        public Guid Id { get; set; }

        [NotEmpty]
        public Guid TypeId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [DateInFuture]
        public DateTime? DueDate { get; set; }
    }
}