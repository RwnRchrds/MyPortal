namespace MyPortal.Dtos
{
    public class PastoralRegGroupDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TutorId { get; set; }

        public int YearGroupId { get; set; }

        public virtual CoreStaffMemberDto Tutor { get; set; }

        public virtual PastoralYearGroupDto PastoralYearGroup { get; set; }
    }
}