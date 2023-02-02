using System;
using System.Collections.Generic;
using MyPortal.Logic.Models.Summary;

namespace MyPortal.Logic.Models.Data.Assessment.MarksheetEntry;

public class MarksheetStudentDataModel
{
    public Guid StudentId { get; set; }
    public string StudentName { get; set; }
    public ICollection<ResultSummaryModel> Results { get; set; }
}