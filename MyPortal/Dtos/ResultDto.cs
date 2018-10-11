namespace MyPortal.Dtos
{
    public class ResultDto
    {
        public int ResultSetId { get; set; }
        public int SubjectId { get; set; }
        public string Value { get; set; }
        public int StudentId { get; set; }
        public SubjectDto Subject { get; set; }
    }
}