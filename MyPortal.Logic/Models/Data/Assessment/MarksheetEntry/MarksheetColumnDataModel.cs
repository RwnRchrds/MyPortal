using System;
using System.Collections.Generic;

namespace MyPortal.Logic.Models.Data.Assessment.MarksheetEntry;

public class MarksheetColumnDataModel
{
    public Guid AspectTypeId { get; set; }
    public Guid AspectId { get; set; }
    public Guid ResultSetId { get; set; }

    public string Header { get; set; }
    public string ResultSetName { get; set; }
    public int Order { get; set; }
    public bool IsReadOnly { get; set; }

    public ICollection<GradeModel> Grades { get; set; }
    public decimal? MinMark { get; set; }
    public decimal? MaxMark { get; set; }
}