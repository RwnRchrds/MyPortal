using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Exceptions;
using MyPortal.Interfaces;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;

namespace MyPortal.Services
{
    public class SystemService : MyPortalService
    {
        public SystemService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }


        public async Task CreateBulletin(SystemBulletin bulletin, string userId, bool autoApprove = false)
        {
            using (var staffService = new StaffMemberService(UnitOfWork))
            {
                if (!ValidationService.ModelIsValid(bulletin))
                {
                    throw new ServiceException(ExceptionType.BadRequest, "Invalid data");
                }

                var author = await staffService.GetStaffMemberByUserId(userId);

                bulletin.CreateDate = DateTime.Today;
                bulletin.Approved = autoApprove;
                bulletin.AuthorId = author.Id;

                if (bulletin.ExpireDate == null)
                {
                    bulletin.ExpireDate = bulletin.CreateDate.AddDays(7);
                }

                UnitOfWork.SystemBulletins.Add(bulletin);

                await UnitOfWork.Complete();
            }
        }

        public async Task DeleteBulletin(int bulletinId)
        {
            var bulletinInDb = await GetBulletinById(bulletinId);

            UnitOfWork.SystemBulletins.Remove(bulletinInDb);

            await UnitOfWork.Complete();
        }

        public async Task<IEnumerable<SystemBulletin>> GetAllBulletins()
        {
            var bulletins = await UnitOfWork.SystemBulletins.GetAll();

            return bulletins;
        }

        public async Task<IEnumerable<SystemBulletin>> GetApprovedBulletins()
        {
            var bulletins = await UnitOfWork.SystemBulletins.GetApproved();

            return bulletins;
        }

        public async Task<IEnumerable<SystemBulletin>> GetApprovedStudentBulletins()
        {
            var bulletins = await UnitOfWork.SystemBulletins.GetStudent();

            return bulletins;
        }

        
        public async Task<IEnumerable<SystemBulletin>> GetOwnBulletins(int authorId)
        {
            var bulletins = await UnitOfWork.SystemBulletins.GetOwn(authorId);

            return bulletins;
        }

        public async Task<SystemBulletin> GetBulletinById(int bulletinId)
        {
            var bulletin = await UnitOfWork.SystemBulletins.GetById(bulletinId);

            if (bulletin == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Bulletin not found");
            }

            return bulletin;
        }

        public async Task UpdateBulletin(SystemBulletin bulletin, bool approvable = false)
        {
            var bulletinInDb = await GetBulletinById(bulletin.Id);

            bulletinInDb.Title = bulletin.Title;
            bulletinInDb.Detail = bulletin.Detail;
            bulletinInDb.ExpireDate = bulletin.ExpireDate;
            bulletinInDb.ShowStudents = bulletin.ShowStudents;
            bulletinInDb.Approved = approvable && bulletin.Approved;

            await UnitOfWork.Complete();
        }

        public async Task<IEnumerable<SchoolLocation>> GetLocations()
        {
            var locations = await UnitOfWork.SchoolLocations.GetAll();

            return locations;
        }

        public async Task<School> GetLocalSchool()
        {
            var school = await UnitOfWork.Schools.GetLocal();

            return school;
        }
    }
}