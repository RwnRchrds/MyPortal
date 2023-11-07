using System.Collections.Generic;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.QueryResults.School;

namespace MyPortal.Database.Models.Paging;

public class BulletinMetadataPageResponse : IPageResponse
{
    public BulletinMetadataPageResponse(IEnumerable<BulletinDetailModel> data, int totalRecords)
    {
        Data = data;
        TotalRecords = totalRecords;
    }

    public IEnumerable<BulletinDetailModel> Data { get; }
    public int TotalRecords { get; }
}