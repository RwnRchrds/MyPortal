using System;

namespace MyPortal.Logic.Models.Requests.Curriculum.Homework;

public class HomeworkSubmissionRequestModel
{
    public Guid HomeworkId { get; set; }
    public Guid StudentId { get; set; }
    public Guid AssignedById { get; set; }
    public DateTime? DueDate { get; set; }
    public Guid? DocumentId { get; set; }
    public bool Completed { get; set; }
    public int PointsAchieved { get; set; }
    public string Comments { get; set; }
}