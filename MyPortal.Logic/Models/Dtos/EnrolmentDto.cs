namespace MyPortal.Logic.Models.Dtos
{
    public class EnrolmentDto
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int ClassId { get; set; }

        public virtual ClassDto Class { get; set; }

        public virtual StudentDto Student { get; set; }
    }
}
