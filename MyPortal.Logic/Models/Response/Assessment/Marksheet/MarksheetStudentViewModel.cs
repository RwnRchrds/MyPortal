using System;
using System.Collections.Generic;
using MyPortal.Logic.Models.Summary;

namespace MyPortal.Logic.Models.Response.Assessment.Marksheet;

public class MarksheetStudentViewModel
{
    public Guid StudentId { get; set; }
    public string StudentName { get; set; }
    public ICollection<ResultSummaryModel> Results { get; set; }
}