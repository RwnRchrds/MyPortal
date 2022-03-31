using System;

namespace MyPortal.Logic.Models.Requests.Assessment;

public class CreateResultEmbargoModel
{
    public Guid ResultSetId { get; set; }
    public DateTime EndDate { get; set; }
}