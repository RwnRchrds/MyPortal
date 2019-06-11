namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// A student in the system.
    /// </summary>
    public partial class StudentDto
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public int RegGroupId { get; set; }

        public int YearGroupId { get; set; }

        [StringLength(10)]
        public string CandidateNumber { get; set; }

        public decimal AccountBalance { get; set; }

        public bool? FreeSchoolMeals { get; set; }

        public bool? GiftedAndTalented { get; set; }

        public int? SenStatusId { get; set; }

        public bool? PupilPremium { get; set; }

        [StringLength(255)]
        public string MisId { get; set; }

        public bool? Deleted { get; set; }

        public virtual PastoralRegGroupDto PastoralRegGroup { get; set; }

        public virtual PastoralYearGroupDto PastoralYearGroup { get; set; }

        public virtual PersonDto Person { get; set; }

        public virtual SenStatusDto SenStatus { get; set; }

    }
}
