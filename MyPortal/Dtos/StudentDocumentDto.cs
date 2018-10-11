namespace MyPortal.Dtos
{
    public class StudentDocumentDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int DocumentId { get; set; }
        public DocumentDto Document { get; set; }
        public StudentDto Student { get; set; }
    }
}