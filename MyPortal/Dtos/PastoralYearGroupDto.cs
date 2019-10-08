namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// A year group in the system.
    /// </summary>
    
    public partial class PastoralYearGroupDto
    {
        public int Id { get; set; }

        
        public string Name { get; set; }

        public int HeadId { get; set; }

        public int KeyStage { get; set; }

        public virtual StaffMemberDto HeadOfYear { get; set; }

        
        

        
        

        
        

        
        
    }
}
