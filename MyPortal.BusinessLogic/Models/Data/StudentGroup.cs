using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.BusinessLogic.Models.Data
{
    public enum StudentGroupType
    {
        RegGroup,
        YearGroup,
        House,
        Class,
        GiftedTalented,
        SenStatus,
        UserDefined
    }
    public class StudentGroup
    {
        public StudentGroupType StudentGroupType { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
