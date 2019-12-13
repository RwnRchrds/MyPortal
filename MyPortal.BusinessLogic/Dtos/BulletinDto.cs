using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.BusinessLogic.Dtos
{
    public class BulletinDto
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
