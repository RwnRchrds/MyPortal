using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.BusinessLogic.Dtos;
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

        public async Task CreateBulletin(BulletinDto bulletin, string userId, bool autoApprove = false)
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

                UnitOfWork.Bulletins.Add(Mapper.Map<Bulletin>(bulletin));

                
            }
        }

        public async Task DeleteBulletin(int bulletinId)
        {
            var bulletinInDb = await UnitOfWork.Bulletins.GetById(bulletinId);

            if (bulletinInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Bulletin not found.");
            }

            UnitOfWork.Bulletins.Remove(bulletinInDb);

            
        }

        public async Task<IEnumerable<BulletinDto>> GetAllBulletins()
        {
            return (await UnitOfWork.Bulletins.GetAll()).Select(Mapper.Map<BulletinDto>).ToList();
        }

        public async Task<IEnumerable<BulletinDto>> GetApprovedBulletins()
        {
            return (await UnitOfWork.Bulletins.GetApproved()).Select(Mapper.Map<BulletinDto>).ToList();
        }

        public async Task<IEnumerable<BulletinDto>> GetApprovedStudentBulletins()
        {
            return (await UnitOfWork.Bulletins.GetStudent()).Select(Mapper.Map<BulletinDto>).ToList();
        }

        
        public async Task<IEnumerable<BulletinDto>> GetOwnBulletins(int authorId)
        {
            return (await UnitOfWork.Bulletins.GetOwn(authorId)).Select(Mapper.Map<BulletinDto>).ToList();
        }

        public async Task<BulletinDto> GetBulletinById(int bulletinId)
        {
            var bulletin = await UnitOfWork.Bulletins.GetById(bulletinId);

            if (bulletin == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Bulletin not found");
            }

            return Mapper.Map<BulletinDto>(bulletin);
        }

        public async Task UpdateBulletin(BulletinDto bulletin, bool approvable = false)
        {
            var bulletinInDb = await UnitOfWork.Bulletins.GetById(bulletin.Id);

            if (bulletinInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Bulletin not found.");
            }

            bulletinInDb.Title = bulletin.Title;
            bulletinInDb.Detail = bulletin.Detail;
            bulletinInDb.ExpireDate = bulletin.ExpireDate;
            bulletinInDb.ShowStudents = bulletin.ShowStudents;
            bulletinInDb.Approved = approvable && bulletin.Approved;

            
        }

        public async Task<IEnumerable<LocationDto>> GetLocations()
        {
            return (await UnitOfWork.Locations.GetAll()).Select(Mapper.Map<LocationDto>).ToList();
        }

        public async Task<SchoolDto> GetLocalSchool()
        {
            var school = await UnitOfWork.Schools.GetLocal();

            return Mapper.Map<SchoolDto>(school);
        }
    }
}