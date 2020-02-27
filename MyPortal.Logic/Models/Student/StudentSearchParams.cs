using System;
using MyPortal.Logic.Models.Person;

namespace MyPortal.Logic.Models.Student
{
    public class StudentSearchParams : PersonSearchParams
    {
        public Guid RegGroupId { get; set; }
        public Guid YearGroupId { get; set; }
        public Guid HouseId { get; set; }
        public Guid SenStatusId { get; set; }
    }
}