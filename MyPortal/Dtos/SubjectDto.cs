using MyPortal.Models;

namespace MyPortal.Dtos
{
    public class SubjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LeaderId { get; set; }
        public string Code { get; set; }

        public StaffDto Staff { get; set; }
    }
}