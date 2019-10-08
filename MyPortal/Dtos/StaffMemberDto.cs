namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// A staff member in the system.
    /// </summary>
    
    public class StaffMemberDto
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        
        public string Code { get; set; }

        public bool Deleted { get; set; }

        public virtual PersonDto Person { get; set; }

        
        

        
        

        
        

        
        

        
        
    }
}
