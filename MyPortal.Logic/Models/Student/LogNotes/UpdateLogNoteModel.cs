using System;
using MyPortal.Logic.Attributes;

namespace MyPortal.Logic.Models.Student.LogNotes
{
    public class UpdateLogNoteModel : CreateLogNoteModel
    {
        [NotEmpty]
        public Guid Id { get; set; }
    }
}
