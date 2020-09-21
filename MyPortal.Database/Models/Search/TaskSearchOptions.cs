namespace MyPortal.Database.Models.Search
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
