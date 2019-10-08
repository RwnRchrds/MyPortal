namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Represents an academic year in the system.
    /// </summary>
    
    public partial class CurriculumAcademicYearDto
    {

        public int Id { get; set; }

        
        public string Name { get; set; }

        
        public DateTime FirstDate { get; set; }

        
        public DateTime LastDate { get; set; }

        
        

        
        

        
        

        
        

        
        
        
        
        
        
    }
}
