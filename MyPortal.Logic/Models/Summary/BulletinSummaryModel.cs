using System;
using System.Threading.Tasks;
using MyPortal.Database.Models.QueryResults.School;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Models.Summary;

public class BulletinSummaryModel
{
    public Guid Id { get; set; }
    public Guid DirectoryId { get; set; }
    public string Title { get; set; }
    public string Detail { get; set; }
    public bool Private { get; set; }
    public bool Approved { get; set; }
    public string CreatedByName { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ExpireDate { get; set; }

    public BulletinSummaryModel(BulletinMetadata metadata)
    {
        Id = metadata.Id;
        DirectoryId = metadata.DirectoryId;
        Title = metadata.Title;
        Detail = metadata.Detail;
        Private = metadata.Private;
        Approved = metadata.Approved;
        CreatedByName = metadata.CreatedByName;
        CreatedDate = metadata.CreatedDate;
        ExpireDate = metadata.ExpireDate;
    }
}