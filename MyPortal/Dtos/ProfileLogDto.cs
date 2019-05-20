using System;

namespace MyPortal.Dtos
{
    public class ProfileLogDto
    {
        public int Id { get; set; }

        public int TypeId { get; set; }

        public int AuthorId { get; set; }

        public int StudentId { get; set; }
        
        public string Message { get; set; }

        public DateTime Date { get; set; }

        public StaffMemberDto Author { get; set; }

        public StudentDto Student { get; set; }

        public ProfileLogTypeDto ProfileLogType { get; set; }
    }
}