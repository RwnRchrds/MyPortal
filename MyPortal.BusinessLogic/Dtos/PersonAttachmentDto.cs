using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.BusinessLogic.Dtos
{
    public class PersonAttachmentDto
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public int DocumentId { get; set; }

        public virtual DocumentDto Document { get; set; }

        public virtual PersonDto Person { get; set; }
    }
}
