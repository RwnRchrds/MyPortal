using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class HomeworkAttachmentDto
    {
        public Guid Id { get; set; }
        public Guid HomeworkId { get; set; }
        public Guid DocumentId { get; set; }

        public virtual HomeworkDto Homework { get; set; }
        public virtual DocumentDto Document { get; set; }
    }
}
