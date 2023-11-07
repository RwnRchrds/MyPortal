using System;

namespace MyPortal.Logic.Models.Requests.Curriculum;

public class CurriculumBandRequestModel : StudentGroupRequestModel
{
    public Guid AcademicYearId { get; set; }

    public Guid CurriculumYearGroupId { get; set; }
}