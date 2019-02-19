using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Models
{
    [Table("Core_Documents_Students")]
    public class StudentDocument
    {
        [Display(Name = "ID")] public int Id { get; set; }

        [Display(Name = "Student")] public int StudentId { get; set; }

        [Display(Name = "Document")] public int DocumentId { get; set; }

        public virtual Document Document { get; set; }

        public virtual Student Student { get; set; }
    }
}