namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// A set of results awarded to students. Result sets usually represent a time-frame eg 'Spring Term'.
    /// </summary>
    
    public class AssessmentResultSetDto
    {

        public int Id { get; set; }

        
        public string Name { get; set; }

        public bool IsCurrent { get; set; }

        
        
    }
}
