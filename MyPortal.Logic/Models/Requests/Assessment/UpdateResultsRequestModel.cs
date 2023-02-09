using MyPortal.Logic.Models.Summary;

namespace MyPortal.Logic.Models.Requests.Assessment;

public class UpdateResultsRequestModel
{
    public ResultSummaryModel[] Results { get; set; }
}