using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("Bulletin")]
    public class Bulletin
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }

        [Display(Name = "Created")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Expires")]
        public DateTime? ExpireDate { get; set; }

        [Required]
        [StringLength(128)]
        public string Title { get; set; }

        [Required]
        public string Detail { get; set; }

        public bool ShowStudents { get; set; }
        
        public bool Approved { get; set; }

        public virtual StaffMember Author { get; set; }
    }
}