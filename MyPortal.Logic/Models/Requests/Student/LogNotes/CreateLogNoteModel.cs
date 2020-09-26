using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Attributes;

namespace MyPortal.Logic.Models.Requests.Student.LogNotes
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
