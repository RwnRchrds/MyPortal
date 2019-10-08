namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    ///  Codes available to use when taking the register.
    /// </summary>
    
    public partial class AttendanceCodeDto
    {
        //THIS IS A SYSTEM CLASS AND SHOULD NOT HAVE FEATURES TO ADD, MODIFY OR DELETE DATABASE OBJECTS
        public int Id { get; set; }

        
        
        public string Code { get; set; }

        
        public string Description { get; set; }

        public int MeaningId { get; set; }

        public bool System { get; set; }

        public bool DoNotUse { get; set; }

        public virtual AttendanceMeaningDto AttendanceMeaning { get; set; }
    }
}
