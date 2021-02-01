using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Requests.Curriculum
{
    public class CreateTermWeekPatternModel
    {
        public Guid WeekPatternId { get; set; }
        public int Order { get; set; }
    }
}
