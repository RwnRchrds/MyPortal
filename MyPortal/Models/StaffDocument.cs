using System.ComponentModel.DataAnnotations;

namespace MyPortal.Models
{
    public class StaffDocument
    {
        public int Id { get; set; }

        [StringLength(3)] public string Staff { get; set; }

        public int Document { get; set; }

        public virtual Document Document1 { get; set; }

        public virtual Staff Staff1 { get; set; }
    }
}