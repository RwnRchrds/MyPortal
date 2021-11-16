using System;
using MyPortal.Database.Enums;

namespace MyPortal.Database.Models.Search
{
    public class StudentSearchOptions : PersonSearchOptions
    {
        public StudentSearchOptions()
        {
            Status = StudentStatus.OnRoll;
        }

        public StudentStatus Status { get; set; }
        public Guid? CurriculumGroupId { get; set; }
        public Guid? RegGroupId { get; set; }
        public Guid? YearGroupId { get; set; }
        public Guid? HouseId { get; set; }
        public Guid? SenStatusId { get; set; }
    }
}
