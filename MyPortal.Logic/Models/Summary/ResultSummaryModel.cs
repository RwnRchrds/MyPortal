using System;

namespace MyPortal.Logic.Models.Summary;

public class ResultSummaryModel
{
    public Guid StudentId { get; set; }
    public Guid AspectId { get; set; }
    public Guid ResultSetId { get; set; }

    public Guid? GradeId { get; set; }
    public decimal? Mark { get; set; }
    public string Comment { get; set; }

    public string ColourCode { get; set; }
    public string Note { get; set; }
}