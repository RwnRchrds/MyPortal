namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    ///  A category of log notes for students.
    /// </summary>
    
    public partial class ProfileLogTypeDto
    {
        //THIS IS A SYSTEM CLASS AND SHOULD NOT HAVE FEATURES TO ADD, MODIFY OR DELETE DATABASE OBJECTS
        
        public int Id { get; set; }

        
        public string Name { get; set; }

        public bool System { get; set; }

        
        
    }
}
