namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// A set of grades to use when adding results.
    /// </summary>
    public partial class AssessmentGradeSetDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsKs4 { get; set; }

        public bool Active { get; set; }
    }
}
