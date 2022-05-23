using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Attributes;

namespace MyPortal.Logic.Models.Requests.Person.Tasks
{
    public class CreateTaskRequestModel
    {
        [NotEmpty]
        public Guid AssignedToId { get; set; }
        
        [NotEmpty] 
        public Guid AssignedById { get; set; }

        [NotEmpty] 
        public Guid TypeId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [FutureDate]
        public DateTime? DueDate { get; set; }
    }
}