using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class HomeworkAttachmentDto
    {
        public int Id { get; set; }
        public int HomeworkId { get; set; }
        public int DocumentId { get; set; }

        public virtual HomeworkDto Homework { get; set; }
        public virtual DocumentDto Document { get; set; }
    }
}
