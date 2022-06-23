using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.School.Bulletins;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class SchoolService : BaseService, ISchoolService
    {
        private async Task<string> GetLocalSchoolNameFromDb()
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var localSchoolName = await unitOfWork.Schools.GetLocalSchoolName();

                return localSchoolName;
            }
        }

        public async Task<string> GetLocalSchoolName()
        {
            var localSchoolName =
                await CacheHelper.StringCache.GetOrCreate(CacheKeys.LocalSchoolName, GetLocalSchoolNameFromDb);

            return localSchoolName;
        }

        public async Task<IEnumerable<BulletinModel>> GetBulletins(BulletinSearchOptions searchOptions)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var bulletins = await unitOfWork.Bulletins.GetBulletins(searchOptions);

                return bulletins.Select(b => new BulletinModel(b));
            }
        }

        public async Task CreateBulletin(params CreateBulletinRequestModel[] models)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var model in models)
                {
                    var bulletin = new Bulletin
                    {
                        Title = model.Title,
                        Detail = model.Detail,
                        CreatedDate = DateTime.Now,
                        CreatedById = model.CreatedById,
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
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdateBulletin(params UpdateBulletinRequestModel[] models)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var model in models)
                {
                    var bulletin = await unitOfWork.Bulletins.GetById(model.Id);

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
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteBulletin(params Guid[] bulletinIds)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var bulletinId in bulletinIds)
                {
                    var bulletin = await unitOfWork.Bulletins.GetById(bulletinId);

                    if (bulletin == null)
                    {
                        throw new NotFoundException("Bulletin not found.");
                    }

                    await unitOfWork.Bulletins.Delete(bulletinId);
                }
            }
        }

        public async Task SetBulletinApproved(params ApproveBulletinRequestModel[] models)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var model in models)
                {
                    var bulletin = await unitOfWork.Bulletins.GetById(model.BulletinId);

                    if (bulletin == null)
                    {
                        throw new NotFoundException("Bulletin not found");
                    }

                    bulletin.Approved = model.Approved;
                }

                await unitOfWork.SaveChangesAsync();
            }
        }
    }
}