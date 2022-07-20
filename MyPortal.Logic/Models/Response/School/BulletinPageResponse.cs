using System.Collections.Generic;
using MyPortal.Database.Models.Paging;
using MyPortal.Database.Models.QueryResults.School;

namespace MyPortal.Logic.Models.Response.School;

public class BulletinPageResponse
{
    public BulletinPageResponse(BulletinMetadataPageResponse pageResponse)
    {
        Bulletins = pageResponse.Data;
        TotalRecords = pageResponse.TotalRecords;
    }
    
    public IEnumerable<BulletinMetadata> Bulletins { get; set; }
    public int TotalRecords { get; set; }
}