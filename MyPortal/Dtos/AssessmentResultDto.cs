namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    
    
    
    
    public class AssessmentResultDto
    {
        public int Id { get; set; }

        public int ResultSetId { get; set; }

        public int StudentId { get; set; }

        public int AspectId { get; set; }

        
        public DateTime Date { get; set; }

        
        public string Grade { get; set; }

        public virtual AssessmentResultSetDto ResultSet { get; set; }

        public virtual AssessmentAspectDto Aspect { get; set; }

        public virtual StudentDto Student { get; set; }
    }
}
