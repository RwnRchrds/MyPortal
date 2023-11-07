using System;

namespace MyPortal.Logic.Models.Requests.Curriculum
{
    public class CreateTermWeekPatternRequestModel
    {
        public Guid WeekPatternId { get; set; }
        public int Order { get; set; }
    }
}