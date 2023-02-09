using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Attributes;

namespace MyPortal.Logic.Models.Requests.Student.LogNotes
{
    public class LogNoteRequestModel
    {
        [NotDefault]
        public Guid StudentId { get; set; }
        
        [NotDefault]
        public Guid TypeId { get; set; }

        [NotDefault]
        public Guid AcademicYearId { get; set; }
        
        [NotDefault] 
        public Guid CreatedById { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
