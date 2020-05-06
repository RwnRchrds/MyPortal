using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Logic.Attributes;

namespace MyPortal.Logic.Models.Student.LogNote
{
    public class UpdateLogNoteModel : CreateLogNoteModel
    {
        [NotEmpty]
        public Guid Id { get; set; }
    }
}
