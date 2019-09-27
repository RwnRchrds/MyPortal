namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// A particular topic for study in the curriculum. A study topic contains lesson plans for delivery.
    /// </summary>
    public class CurriculumStudyTopicDto
    {
        public int Id { get; set; }

        public int SubjectId { get; set; }

        public int YearGroupId { get; set; }

        public string Name { get; set; }

        public virtual CurriculumSubjectDto CurriculumSubject { get; set; }

        public virtual PastoralYearGroupDto PastoralYearGroup { get; set; }
    }
}
