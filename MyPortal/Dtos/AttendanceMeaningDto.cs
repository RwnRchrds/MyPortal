namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    
    /// <summary>
    ///  Meanings of register codes in the system.
    /// </summary>
    
    public partial class AttendanceMeaningDto
    {
        //THIS IS A SYSTEM CLASS AND SHOULD NOT HAVE FEATURES TO ADD, MODIFY OR DELETE DATABASE OBJECTS

        public int Id { get; set; }

        
        public string Code { get; set; }

        
        public string Description { get; set; }

        
        
    }
}
