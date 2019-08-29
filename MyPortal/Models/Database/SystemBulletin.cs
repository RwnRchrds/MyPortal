using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MyPortal.Models.Database
{
    [Table("System_Bulletins")]
    public class SystemBulletin
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }

        [Display(Name = "Created")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Expires")]
        public DateTime? ExpireDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        public string Detail { get; set; }

        public bool ShowStudents { get; set; }
        
        public bool Approved { get; set; }

        public virtual StaffMember Author { get; set; }
    }
}