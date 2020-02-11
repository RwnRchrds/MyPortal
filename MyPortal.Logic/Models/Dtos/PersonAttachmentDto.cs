using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class PersonAttachmentDto
    {
        public Guid Id { get; set; }

        public Guid PersonId { get; set; }

        public Guid DocumentId { get; set; }

        public virtual DocumentDto Document { get; set; }

        public virtual PersonDto Person { get; set; }
    }
}
