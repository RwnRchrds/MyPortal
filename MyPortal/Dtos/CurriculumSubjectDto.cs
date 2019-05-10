namespace MyPortal.Dtos
{
    public class CurriculumSubjectDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int LeaderId { get; set; }

        public string Code { get; set; }

        public StaffMemberDto Leader { get; set; }
    }
}