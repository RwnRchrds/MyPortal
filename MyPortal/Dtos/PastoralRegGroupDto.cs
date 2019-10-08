namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// A registration/tutor group in the system.
    /// </summary>
    
    public partial class PastoralRegGroupDto
    {
        public int Id { get; set; }

        
        public string Name { get; set; }

        public int TutorId { get; set; }

        public int YearGroupId { get; set; }

        public virtual StaffMemberDto Tutor { get; set; }

        public virtual PastoralYearGroupDto PastoralYearGroup { get; set; }
    }
}
