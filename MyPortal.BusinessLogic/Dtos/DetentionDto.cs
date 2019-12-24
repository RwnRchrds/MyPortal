using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.BusinessLogic.Dtos
{
    public class DetentionDto
    {
        public int Id { get; set; }
        public int DetentionTypeId { get; set; }
        public int EventId { get; set; }

        public DetentionTypeDto Type { get; set; }
    }
}
