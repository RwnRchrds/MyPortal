using System;
using MyPortal.Logic.Models.Requests.Person;

namespace MyPortal.Logic.Models.Requests.Student
{
    public class StudentSearchModel : PersonSearchModel
    {
        public Guid? SearchType { get; set; }
        public Guid? RegGroupId { get; set; }
        public Guid? YearGroupId { get; set; }
        public Guid? HouseId { get; set; }
        public Guid? SenStatusId { get; set; }
    }
}