using System;
using MyPortal.Logic.Enums;
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
    public DateTime? ExpireDate { get; set; }

    public BulletinSummaryModel(BulletinModel bulletin)
    {
        Id = bulletin.Id.Value;
        DirectoryId = bulletin.DirectoryId;
        Title = bulletin.Title;
        Detail = bulletin.Detail;
        Private = bulletin.Private;
        Approved = bulletin.Approved;
        CreatedByName = bulletin.CreatedBy.GetDisplayName(NameFormat.FullNameAbbreviated);
        ExpireDate = bulletin.ExpireDate;
    }
}