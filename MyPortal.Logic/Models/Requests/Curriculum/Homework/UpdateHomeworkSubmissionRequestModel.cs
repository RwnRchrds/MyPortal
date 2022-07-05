using System;

namespace MyPortal.Logic.Models.Requests.Curriculum.Homework;

public class UpdateHomeworkSubmissionRequestModel
{
    public Guid Id { get; set; }
    public DateTime? DueDate { get; set; }
    public int PointsAchieved { get; set; }
    public string Comments { get; set; }
    public bool Completed { get; set; }
}