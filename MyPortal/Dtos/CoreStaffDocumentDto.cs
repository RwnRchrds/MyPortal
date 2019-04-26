namespace MyPortal.Dtos
{
    public class CoreStaffDocumentDto
    {
        public int Id { get; set; }

        public int StaffId { get; set; }

        public int DocumentId { get; set; }

        public virtual CoreDocumentDto CoreDocument { get; set; }

        public virtual CoreStaffMemberDto Owner { get; set; }
    }
}