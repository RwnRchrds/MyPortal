using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.BusinessLogic.Dtos
{
    public class DocumentDto
    {
        public int Id { get; set; }

        public int TypeId { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        [Required]
        [Url]
        public string Url { get; set; }

        public int UploaderId { get; set; }

        public DateTime Date { get; set; }

        public bool IsGeneral { get; set; }

        public bool Approved { get; set; }

        public bool Deleted { get; set; }

        public virtual StaffMemberDto Uploader { get; set; }

        public virtual DocumentTypeDto Type { get; set; }
    }
}
