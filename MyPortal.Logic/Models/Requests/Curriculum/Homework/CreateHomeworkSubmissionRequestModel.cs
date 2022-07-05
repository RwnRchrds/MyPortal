using System;

namespace MyPortal.Logic.Models.Requests.Curriculum.Homework;

public class CreateHomeworkSubmissionRequestModel
{
    public Guid HomeworkId { get; set; }
    public Guid StudentId { get; set; }
    public Guid AssignedById { get; set; }
    public DateTime? DueDate { get; set; }
}