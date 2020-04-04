using System;

namespace MyPortal.Logic.Models.Business
{
    public class HomeworkModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool SubmitOnline { get; set; }
    }
}
