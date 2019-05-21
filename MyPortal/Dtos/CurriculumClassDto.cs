namespace MyPortal.Dtos
{
    public class CurriculumClassDto
    {
        public int Id { get; set; }

        public int AcademicYearId { get; set; }

        public int? SubjectId { get; set; }

        public string Name { get; set; }

        public int TeacherId { get; set; }

        public int? YearGroupId { get; set; }

        public StaffMemberDto Teacher { get; set; }

        public CurriculumAcademicYearDto CurriculumAcademicYear { get; set; }

        public CurriculumSubjectDto CurriculumSubject { get; set; }

        public PastoralYearGroupDto PastoralYearGroup { get; set; }

    }
}