namespace MyPortal.Models
{
    public class StudentDocument
    {
        public int Id { get; set; }

        public int Student { get; set; }

        public int Document { get; set; }

        public virtual Document Document1 { get; set; }

        public virtual Student Student1 { get; set; }
    }
}