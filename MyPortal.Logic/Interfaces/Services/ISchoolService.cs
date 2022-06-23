using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.School;
using MyPortal.Logic.Models.Requests.School.Bulletins;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface ISchoolService
    {
        Task<string> GetLocalSchoolName();
        Task<IEnumerable<BulletinModel>> GetBulletins(BulletinSearchOptions searchOptions);
        Task CreateBulletin(params CreateBulletinRequestModel[] models);
        Task UpdateBulletin(params UpdateBulletinRequestModel[] models);
        Task DeleteBulletin(params Guid[] bulletinIds);
        Task SetBulletinApproved(params ApproveBulletinRequestModel[] models);
    }
}
