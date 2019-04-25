namespace MyPortal.Dtos
{
    public class CoreStudentDocumentDto
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int DocumentId { get; set; }

        public CoreDocumentDto CoreDocument { get; set; }

        public CoreStudentDto CoreStudent { get; set; }
    }
}