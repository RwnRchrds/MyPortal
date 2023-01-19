using System;

namespace MyPortal.Database.Models.QueryResults.Assessment;

public class MarksheetDetailModel
{
    public Guid MarksheetId { get; set; }
    public Guid MarksheetTemplateId { get; set; }
    public Guid StudentGroupId { get; set; }
    public bool Completed { get; set; }

    public string StudentGroupCode { get; set; }
    public string TemplateName { get; set; }
    public Guid? OwnerId { get; set; }
    public string OwnerName { get; set; }
}