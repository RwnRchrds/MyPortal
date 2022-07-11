using System;

namespace MyPortal.Database.Models.Search;

public class HomeworkSearchOptions
{
    public Guid? SubjectId { get; set; }
    public Guid? CourseId { get; set; }
    public string SearchText { get; set; }
}