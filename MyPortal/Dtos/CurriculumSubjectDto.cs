namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// A list of subjects/courses in the system.
    /// </summary>
    public partial class CurriculumSubjectDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int LeaderId { get; set; }

        public string Code { get; set; }

        public bool Deleted { get; set; }

        public virtual StaffMemberDto Leader { get; set; }
    }
}
