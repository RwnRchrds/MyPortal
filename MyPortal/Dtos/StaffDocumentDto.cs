namespace MyPortal.Dtos
{
    public class StaffDocumentDto
    {
        public int Id { get; set; }
        public string Staff { get; set; }
        public int Document { get; set; }
        public DocumentDto Document1 { get; set; }
        public StaffDto Staff1 { get; set; }
    }
}