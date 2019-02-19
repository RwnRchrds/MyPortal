using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Models
{
    [Table("Core_Documents_Staff")]
    public class StaffDocument
    {
        [Display(Name = "ID")] public int Id { get; set; }

        [Display(Name = "Staff")] public int StaffId { get; set; }

        [Display(Name = "Document")] public int DocumentId { get; set; }

        public virtual Document Document { get; set; }

        public virtual Staff Staff { get; set; }
    }
}