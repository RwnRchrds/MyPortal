using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Attributes;

namespace MyPortal.Logic.Models.Requests.Person.Tasks
{
    public class TaskRequestModel
    {
        [NotDefault]
        public Guid AssignedToId { get; set; }
        
        [NotDefault] 
        public Guid AssignedById { get; set; }

        [NotDefault] 
        public Guid TypeId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
        
        public DateTime? DueDate { get; set; }
    }
}