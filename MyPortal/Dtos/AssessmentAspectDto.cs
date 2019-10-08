using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos
{
    
    public class AssessmentAspectDto
    {        

        public int Id { get; set; }
        public int TypeId { get; set; }
        public int? GradeSetId { get; set; }

        
        public string Description { get; set; }

        public virtual AssessmentAspectTypeDto AspectType { get; set; }
        public virtual AssessmentGradeSetDto GradeSet { get; set; }

        
        
    }
}