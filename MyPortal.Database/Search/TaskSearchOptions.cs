using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Database.Search
{
    public enum TaskStatus
    {
        Active,
        Completed,
        Overdue
    }

    public class TaskSearchOptions
    {
        public TaskStatus Status { get; set; }
    }
}
