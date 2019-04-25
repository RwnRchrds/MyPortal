namespace MyPortal.Dtos
{
    public class PastoralYearGroupDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int HeadId { get; set; }

        public int KeyStage { get; set; }

        public CoreStaffMemberDto CoreStaffMember { get; set; }
    }
}