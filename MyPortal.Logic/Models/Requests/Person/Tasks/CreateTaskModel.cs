﻿using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Attributes;

namespace MyPortal.Logic.Models.Requests.Person.Tasks
{
    public class CreateTaskModel
    {
        [NotEmpty]
        public Guid AssignedToId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [DateInFuture]
        public DateTime? DueDate { get; set; }
    }
}