using System;
using MyPortal.Logic.Attributes;

namespace MyPortal.Logic.Models.Requests.Assessment;

public class ResultHistoryRequestModel
{
    [NotDefault] public Guid StudentId { get; set; }

    [NotDefault] public Guid AspectId { get; set; }

    [NotDefault] public DateTime DateTo { get; set; }
}