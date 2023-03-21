using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Filters;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.School;

using MyPortal.Logic.Models.Requests.School.Bulletins;
using MyPortal.Logic.Models.Summary;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class SchoolService : BaseUserService, ISchoolService
    {
        public SchoolService(ICurrentUser user) : base(user)
        {
        }

        private async Task<string> GetLocalSchoolNameFromDb()
        {
            await using var unitOfWork = await User.GetConnection();
            
            var localSchoolName = await unitOfWork.Schools.GetLocalSchoolName();

            return localSchoolName;
        }

        public async Task<string> GetLocalSchoolName()
        {
            var localSchoolName =
                await CacheHelper.StringCache.GetOrCreate(CacheKeys.LocalSchoolName, GetLocalSchoolNameFromDb,
                    TimeSpan.FromHours(24));

            return localSchoolName;
        }

        public async Task<IEnumerable<BulletinModel>> GetBulletins(BulletinSearchOptions searchOptions)
        {
            await using var unitOfWork = await User.GetConnection();
            
            var bulletins = await unitOfWork.Bulletins.GetBulletins(searchOptions);

            return bulletins.Select(b => new BulletinModel(b));
        }

        public async Task<IEnumerable<BulletinSummaryModel>> GetBulletinSummaries(BulletinSearchOptions searchOptions)
        {
            await using var unitOfWork = await User.GetConnection();
            
            var bulletins = await unitOfWork.Bulletins.GetBulletinDetails(searchOptions);

            return bulletins.Select(b => new BulletinSummaryModel(b));
        }

        public async Task<BulletinPageResponse> GetBulletinSummaries(BulletinSearchOptions searchOptions, PageFilter filter)
        {
            await using var unitOfWork = await User.GetConnection();
            
            var bulletins = await unitOfWork.Bulletins.GetBulletinDetails(searchOptions, filter);

            var response = new BulletinPageResponse(bulletins);

            return response;
        }

        public async Task<BulletinModel> CreateBulletin(BulletinRequestModel model)
        {
            Validate(model);
            
            await using var unitOfWork = await User.GetConnection();
            
            var bulletin = new Bulletin
            {
                Id = Guid.NewGuid(),
                Title = model.Title,
                Detail = model.Detail,
                CreatedDate = DateTime.Now,
                CreatedById = User.GetUserId(),
                ExpireDate = model.ExpireDate,
                Private = model.Private,
                Directory = new Directory
                {
                    Name = "bulletin-root",
                    Private = model.Private
                },
                Approved = false
            };
                    
            unitOfWork.Bulletins.Create(bulletin);

            await unitOfWork.SaveChangesAsync();

            return new BulletinModel(bulletin);
        }

        public async Task UpdateBulletin(Guid bulletinId, BulletinRequestModel model)
        {
            Validate(model);
            
            await using var unitOfWork = await User.GetConnection();
            
            var bulletin = await unitOfWork.Bulletins.GetById(bulletinId);

            if (bulletin == null)
            {
                throw new NotFoundException("Bulletin not found.");
            }

            bulletin.Title = model.Title;
            bulletin.Detail = model.Detail;
            bulletin.ExpireDate = model.ExpireDate;
            bulletin.Private = model.Private;
            bulletin.Directory.Private = model.Private;

            await unitOfWork.Bulletins.Update(bulletin);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteBulletin(Guid bulletinId)
        {
            await using var unitOfWork = await User.GetConnection();
            
            var bulletin = await unitOfWork.Bulletins.GetById(bulletinId);

            if (bulletin == null)
            {
                throw new NotFoundException("Bulletin not found.");
            }

            await unitOfWork.Bulletins.Delete(bulletinId);
        }

        public async Task SetBulletinApproved(ApproveBulletinRequestModel model)
        {
            await using var unitOfWork = await User.GetConnection();
            
            var bulletin = await unitOfWork.Bulletins.GetById(model.BulletinId);

            if (bulletin == null)
            {
                throw new NotFoundException("Bulletin not found");
            }

            bulletin.Approved = model.Approved;

            await unitOfWork.SaveChangesAsync();
        }
    }
}