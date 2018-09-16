using System.ComponentModel.DataAnnotations;

namespace MyPortal.Models
{
    public class StudentDocument
    {
        public int Id { get; set; }

        [Required]
        public int Student { get; set; }

        [Required]
        public int Document { get; set; }

        public virtual Document Document1 { get; set; }

        public virtual Student Student1 { get; set; }
    }
}