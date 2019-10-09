using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Xml.Schema;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using MyPortal.Dtos;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;
using Syncfusion.EJ2.Base;
using Syncfusion.EJ2.Linq;

namespace MyPortal.Processes
{
    public static class SystemProcesses
    {

        public static async Task<ProcessResponse<object>> CreateBulletin(SystemBulletin bulletin, string userId, MyPortalDbContext context, bool autoApprove = false)
        {
            if (!ValidationProcesses.ModelIsValid(bulletin))
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Invalid data", null);
            }

            var authorId = bulletin.Author.Id;

            var author = PeopleProcesses.HandleAuthorFromUserId(userId, authorId, context).ResponseObject;

            bulletin.CreateDate = DateTime.Today;
            bulletin.Approved = autoApprove;
            bulletin.AuthorId = author.Id;

            if (bulletin.ExpireDate == null)
            {
                bulletin.ExpireDate = bulletin.CreateDate.AddDays(7);
            }

            context.SystemBulletins.Add(bulletin);
            await context.SaveChangesAsync();

            return new ProcessResponse<object>(ResponseType.Ok, "Bulletin created", null);
        }

        public static ProcessResponse<object> DeleteBulletin(int bulletinId, MyPortalDbContext context)
        {
            var bulletinInDb = context.SystemBulletins.SingleOrDefault(x => x.Id == bulletinId);

            if (bulletinInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Bulletin not found", null);
            }

            context.SystemBulletins.Remove(bulletinInDb);
            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Bulletin deleted", null);
        }

        public static ProcessResponse<IEnumerable<SystemBulletinDto>> GetAllBulletins(MyPortalDbContext context)
        {
            var bulletins = GetAllBulletins_Model(context).ResponseObject
                .Select(Mapper.Map<SystemBulletin, SystemBulletinDto>);
            return new ProcessResponse<IEnumerable<SystemBulletinDto>>(ResponseType.Ok, null, bulletins);
        }

        public static ProcessResponse<IEnumerable<SystemBulletin>> GetAllBulletins_Model(MyPortalDbContext context)
        {
            var bulletins = context.SystemBulletins.OrderByDescending(x => x.CreateDate).ToList();

            return new ProcessResponse<IEnumerable<SystemBulletin>>(ResponseType.Ok, null, bulletins);
        }

        public static ProcessResponse<IEnumerable<SystemBulletinDto>> GetApprovedBulletins(MyPortalDbContext context)
        {
            var bulletins = GetApprovedBulletins_Model(context).ResponseObject
                .Select(Mapper.Map<SystemBulletin, SystemBulletinDto>);
            return new ProcessResponse<IEnumerable<SystemBulletinDto>>(ResponseType.Ok, null, bulletins);
        }

        public static ProcessResponse<IEnumerable<SystemBulletin>> GetApprovedBulletins_Model(MyPortalDbContext context)
        {
            var bulletins = context.SystemBulletins.Where(x => x.Approved).OrderByDescending(x => x.CreateDate).ToList();

            return new ProcessResponse<IEnumerable<SystemBulletin>>(ResponseType.Ok, null, bulletins);
        }

        public static ProcessResponse<IEnumerable<SystemBulletinDto>> GetApprovedStudentBulletins(MyPortalDbContext context)
        {
            var bulletins = GetApprovedStudentBulletins_Model(context).ResponseObject
                .Select(Mapper.Map<SystemBulletin, SystemBulletinDto>);
            return new ProcessResponse<IEnumerable<SystemBulletinDto>>(ResponseType.Ok, null, bulletins);
        }

        public static ProcessResponse<IEnumerable<SystemBulletin>> GetApprovedStudentBulletins_Model(MyPortalDbContext context)
        {
            var bulletins = context.SystemBulletins.Where(x => x.Approved && x.ShowStudents)
                .OrderByDescending(x => x.CreateDate).ToList();

            return new ProcessResponse<IEnumerable<SystemBulletin>>(ResponseType.Ok, null, bulletins);
        }

        public static int? GetCurrentAcademicYearId(MyPortalDbContext context)
        {
            var academicYear =
                context.CurriculumAcademicYears.SingleOrDefault(x =>
                    x.FirstDate <= DateTime.Today && x.LastDate >= DateTime.Today) ??
                context.CurriculumAcademicYears.FirstOrDefault();

            return academicYear?.Id;
        }

        public static int GetCurrentOrSelectedAcademicYearId(MyPortalDbContext context, IPrincipal user)
        {
            var academicYearId = GetCurrentAcademicYearId(context);
            
            if (user != null && user.HasPermission("AccessStaffPortal"))
            {
                var selectedAcademicYearId = user.GetSelectedAcademicYearId();

                if (selectedAcademicYearId != null)
                {
                    academicYearId = selectedAcademicYearId;
                }
            }

            if (academicYearId == null)
            {
                return 0;
            }

            return (int) academicYearId;
        }
        public static ProcessResponse<IEnumerable<SystemBulletinDto>> GetOwnBulletins(string userId, MyPortalDbContext context)
        {
            var getAuthor = PeopleProcesses.GetStaffFromUserId(userId, context);

            if (getAuthor.ResponseType == ResponseType.NotFound)
            {
                return new ProcessResponse<IEnumerable<SystemBulletinDto>>(ResponseType.NotFound, getAuthor.ResponseMessage,
                    null);
            }

            if (getAuthor.ResponseType == ResponseType.BadRequest)
            {
                return new ProcessResponse<IEnumerable<SystemBulletinDto>>(ResponseType.BadRequest, getAuthor.ResponseMessage, null);
            }

            var bulletins = GetOwnBulletins_Model(getAuthor.ResponseObject.Id, context).ResponseObject
                .Select(Mapper.Map<SystemBulletin, SystemBulletinDto>);
            return new ProcessResponse<IEnumerable<SystemBulletinDto>>(ResponseType.Ok, null, bulletins);
        }

        public static ProcessResponse<IEnumerable<SystemBulletin>> GetOwnBulletins_Model(int authorId, MyPortalDbContext context)
        {
            var bulletins = context.SystemBulletins.Where(x => x.AuthorId == authorId)
                .OrderByDescending(x => x.CreateDate).ToList();

            return new ProcessResponse<IEnumerable<SystemBulletin>>(ResponseType.Ok, null, bulletins);
        }

        public static ProcessResponse<object> UpdateBulletin(SystemBulletin bulletin, MyPortalDbContext context, bool approvable = false)
        {
            var bulletinInDb = context.SystemBulletins.SingleOrDefault(x => x.Id == bulletin.Id);

            if (bulletinInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Bulletin not found", null);
            }

            bulletinInDb.Title = bulletin.Title;
            bulletinInDb.Detail = bulletin.Detail;
            bulletinInDb.ExpireDate = bulletin.ExpireDate;
            bulletinInDb.ShowStudents = bulletin.ShowStudents;
            bulletinInDb.Approved = approvable && bulletin.Approved;

            context.SaveChanges();
            
            return new ProcessResponse<object>(ResponseType.Ok, "Bulletin updated", null);
        }

        public static bool ValidateUpn(string upn)
        {
            var alpha = new[]
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'T', 'U', 'V', 'W', 'X',
                'Y', 'Z'
            };

            var chars = upn.ToCharArray();

            if (chars.Length != 13)
            {
                return false;
            }

            var check = 0;

            for (var i = 1; i < chars.Length; i++)
            {
                var n = char.GetNumericValue(chars[i]) * (i+1);

                check += (int) n;
            }

            var alphaIndex = check % 23;

            return chars[0] == alpha[alphaIndex];
        }
    }
}