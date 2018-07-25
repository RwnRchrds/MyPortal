namespace MyPortal.Dtos
{
    public class StudentDocumentDto
    {
        public int Id { get; set; }
        public int Student { get; set; }
        public int Document { get; set; }
        public DocumentDto Document1 { get; set; }
        public StudentDto Student1 { get; set; }
    }
}