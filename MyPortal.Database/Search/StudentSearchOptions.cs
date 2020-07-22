using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Database.Search
{
    public enum StudentStatus
    {
        Any,
        OnRoll,
        Leavers,
        Future
    }

    public class StudentSearchOptions : PersonSearchOptions
    {
        public StudentSearchOptions()
        {
            Status = StudentStatus.Any;
        }

        public StudentStatus Status { get; set; }
        public Guid? CurriculumGroupId { get; set; }
        public Guid? RegGroupId { get; set; }
        public Guid? YearGroupId { get; set; }
        public Guid? HouseId { get; set; }
        public Guid? SenStatusId { get; set; }
    }
}
