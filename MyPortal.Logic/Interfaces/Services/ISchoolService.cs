using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Filters;
using MyPortal.Database.Models.QueryResults.School;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.School;
using MyPortal.Logic.Models.Requests.School.Bulletins;
using MyPortal.Logic.Models.Response.School;
using MyPortal.Logic.Models.Summary;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface ISchoolService
    {
        Task<string> GetLocalSchoolName();
        Task<IEnumerable<BulletinModel>> GetBulletins(BulletinSearchOptions searchOptions);
        Task<IEnumerable<BulletinSummaryModel>> GetBulletinSummaries(BulletinSearchOptions searchOptions);
        Task<BulletinPageResponse> GetBulletinSummaries(BulletinSearchOptions searchOptions, PageFilter filter);
        Task<BulletinModel> CreateBulletin(BulletinRequestModel bulletin);
        Task UpdateBulletin(Guid bulletinId, BulletinRequestModel bulletin);
        Task DeleteBulletin(Guid bulletinId);
        Task SetBulletinApproved(ApproveBulletinRequestModel model);
    }
}
