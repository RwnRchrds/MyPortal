namespace MyPortal.Dtos
{
    public class PersonnelTrainingCertificateDto
    {
        public int CourseId { get; set; }

        public int StaffId { get; set; }

        public int StatusId { get; set; }

        public CoreStaffMemberDto CoreStaffMember { get; set; }

        public PersonnelTrainingCourseDto PersonnelTrainingCourse { get; set; }

        public PersonnelTrainingStatusDto PersonnelTrainingStatus { get; set; }
    }
}