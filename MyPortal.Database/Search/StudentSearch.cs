using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Database.Search
{
    public class StudentSearch : PersonSearch
    {
        public Guid? RegGroupId { get; set; }
        public Guid? YearGroupId { get; set; }
        public Guid? HouseId { get; set; }
        public Guid? SenStatusId { get; set; }
    }
}
