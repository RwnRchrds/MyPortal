namespace MyPortal.Dtos
{
    public class StaffDocumentDto
    {
        public int Id { get; set; }

        public int StaffId { get; set; }

        public int DocumentId { get; set; }

        public virtual DocumentDto Document { get; set; }

        public virtual StaffMemberDto Owner { get; set; }
    }
}