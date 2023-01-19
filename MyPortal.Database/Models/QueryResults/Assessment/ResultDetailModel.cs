using System;

namespace MyPortal.Database.Models.QueryResults.Assessment;

public class ResultDetailModel
{
    public Guid ResultSetId { get; set; }
    public Guid StudentId { get; set; }
    public Guid AspectId { get; set; }
    public Guid CreatedById { get; set; }
    public DateTime Date { get; set; }
    public Guid? GradeId { get; set; }
    public decimal? Mark { get; set; }
    public string Comment { get; set; }
    public string ColourCode { get; set; }
    public string Note { get; set; }

    public string StudentName { get; set; }
    public string CreatedByName { get; set; }
}