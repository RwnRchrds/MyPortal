using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos.SpecialDtos
{
    public class StudentSearchDto
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string RegGroupName { get; set; }
        public string YearGroupName { get; set; }
        public string HouseName { get; set; }
    }
}