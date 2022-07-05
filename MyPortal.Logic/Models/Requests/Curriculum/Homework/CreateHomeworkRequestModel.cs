using System;

namespace MyPortal.Logic.Models.Requests.Curriculum.Homework;

public class CreateHomeworkRequestModel
{
    public Guid AssignedById { get; set; }
    public Guid[] StudentIds { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool SubmitOnline { get; set; }
    public int MaxPoints { get; set; }
    public DateTime? DueDate { get; set; }
}