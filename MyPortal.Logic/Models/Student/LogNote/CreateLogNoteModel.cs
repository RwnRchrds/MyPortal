using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Logic.Attributes;

namespace MyPortal.Logic.Models.Student.LogNote
{
    public class CreateLogNoteModel
    {
        [NotEmpty]
        public Guid StudentId { get; set; }
        [NotEmpty]
        public Guid TypeId { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
