using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.BusinessLogic.Dtos
{
    public class AttendanceCodeDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool DoNotUse { get; set; }
    }
}
