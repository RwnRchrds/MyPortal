using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.BusinessLogic.Dtos
{
    public class AspectDto
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public int GradeSetId { get; set; }
        public string Description { get; set; }
        public virtual AspectTypeDto Type { get; set; }
        public virtual GradeSetDto GradeSet { get; set; }
    }
}
