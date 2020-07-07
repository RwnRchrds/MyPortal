using System;

namespace MyPortal.Logic.Models.Entity
{
    public class HomeworkModel
    {
        public Guid Id { get; set; }
        public Guid DirectoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool SubmitOnline { get; set; }

        public virtual DirectoryModel Directory { get; set; }
    }
}
