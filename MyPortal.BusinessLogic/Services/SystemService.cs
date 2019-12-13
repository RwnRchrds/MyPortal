using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.BusinessLogic.Exceptions;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.BusinessLogic.Services
{
    public class SystemService : MyPortalService
    {
        public SystemService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public SystemService() : base()
        {

        }

        public async Task CreateBulletin(Bulletin bulletin, string userId, bool autoApprove = false)
        {
            using (var staffService = new StaffMemberService())
            {
                ValidationService.ValidateModel(bulletin);

                var author = await staffService.GetStaffMemberByUserId(userId);

                bulletin.CreateDate = DateTime.Today;
                bulletin.Approved = autoApprove;
                bulletin.AuthorId = author.Id;

                if (bulletin.ExpireDate == null)
                {
                    bulletin.ExpireDate = bulletin.CreateDate.AddDays(7);
                }

                UnitOfWork.Bulletins.Add(bulletin);

                await UnitOfWork.Complete();
            }
        }

        public async Task DeleteBulletin(int bulletinId)
        {
            var bulletinInDb = await GetBulletinById(bulletinId);

            UnitOfWork.Bulletins.Remove(bulletinInDb);

            await UnitOfWork.Complete();
        }

        public async Task<IEnumerable<Bulletin>> GetAllBulletins()
        {
            var bulletins = await UnitOfWork.Bulletins.GetAll();

            return bulletins;
        }

        public async Task<IEnumerable<Bulletin>> GetApprovedBulletins()
        {
            var bulletins = await UnitOfWork.Bulletins.GetApproved();

            return bulletins;
        }

        public async Task<IEnumerable<Bulletin>> GetApprovedStudentBulletins()
        {
            var bulletins = await UnitOfWork.Bulletins.GetStudent();

            return bulletins;
        }

        
        public async Task<IEnumerable<Bulletin>> GetOwnBulletins(int authorId)
        {
            var bulletins = await UnitOfWork.Bulletins.GetOwn(authorId);

            return bulletins;
        }

        public async Task<Bulletin> GetBulletinById(int bulletinId)
        {
            var bulletin = await UnitOfWork.Bulletins.GetById(bulletinId);

            if (bulletin == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Bulletin not found");
            }

            return bulletin;
        }

        public async Task UpdateBulletin(Bulletin bulletin, bool approvable = false)
        {
            var bulletinInDb = await GetBulletinById(bulletin.Id);

            bulletinInDb.Title = bulletin.Title;
            bulletinInDb.Detail = bulletin.Detail;
            bulletinInDb.ExpireDate = bulletin.ExpireDate;
            bulletinInDb.ShowStudents = bulletin.ShowStudents;
            bulletinInDb.Approved = approvable && bulletin.Approved;

            await UnitOfWork.Complete();
        }

        public async Task<IEnumerable<Location>> GetLocations()
        {
            var locations = await UnitOfWork.Locations.GetAll();

            return locations;
        }

        public async Task<School> GetLocalSchool()
        {
            var school = await UnitOfWork.Schools.GetLocal();

            return school;
        }
    }
}