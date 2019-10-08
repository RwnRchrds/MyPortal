using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MyPortal.Dtos
{
    
    public class SystemBulletinDto
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }

        
        public DateTime CreateDate { get; set; }

        
        public DateTime? ExpireDate { get; set; }

        
        public string Title { get; set; }

        
        public string Detail { get; set; }

        public bool ShowStudents { get; set; }
        
        public bool Approved { get; set; }

        public virtual StaffMemberDto Author { get; set; }
    }
}