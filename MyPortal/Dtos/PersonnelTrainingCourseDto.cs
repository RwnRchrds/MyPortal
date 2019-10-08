namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// A training course available to staff.
    /// </summary>
    
    public partial class PersonnelTrainingCourseDto
    {
        public int Id { get; set; }

        
        public string Code { get; set; }

        
        public string Description { get; set; }

        
        
    }
}
