using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos
{
    public class GradeDto
    {
        public int Id { get; set; }
        public int GradeSetId { get; set; }
        public string GradeValue { get; set; }
        public GradeSetDto GradeSet { get; set; }
    }
}