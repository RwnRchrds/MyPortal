using System;
using System.Threading.Tasks;
using MyPortal.Database.Models.QueryResults.School;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Helpers;


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

    public BulletinSummaryModel(BulletinDetailModel detailModel)
    {
        Id = detailModel.Id;
        DirectoryId = detailModel.DirectoryId;
        Title = detailModel.Title;
        Detail = detailModel.Detail;
        Private = detailModel.Private;
        Approved = detailModel.Approved;
        CreatedByName = detailModel.CreatedByName;
        CreatedDate = detailModel.CreatedDate;
        ExpireDate = detailModel.ExpireDate;
    }
}