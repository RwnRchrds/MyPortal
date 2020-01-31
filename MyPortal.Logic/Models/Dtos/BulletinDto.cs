using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class BulletinDto
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }
        public DateTime CreateDate { get; set; }

        public DateTime? ExpireDate { get; set; }

        [Required]
        [StringLength(128)]
        public string Title { get; set; }

        [Required]
        public string Detail { get; set; }

        public bool ShowStudents { get; set; }

        public bool Approved { get; set; }

        public virtual StaffMemberDto Author { get; set; }
    }
}
