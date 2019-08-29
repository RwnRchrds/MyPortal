using System;

namespace MyPortal.Dtos
{
    public class SystemBulletinDto
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }
        
        public DateTime CreateDate { get; set; }
        
        public DateTime ExpireDate { get; set; }
        
        public string Title { get; set; }
        
        public string Detail { get; set; }

        public bool ShowStudents { get; set; }
        
        public bool Approved { get; set; }
    }
}