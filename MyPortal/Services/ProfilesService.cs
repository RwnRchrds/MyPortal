using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Exceptions;
using MyPortal.Interfaces;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;

namespace MyPortal.Services
{
    public class ProfilesService : MyPortalService
    {
        public ProfilesService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            
        }

        public ProfilesService() : base()
        {

        }

        public async Task CreateComment(ProfileComment comment)
        {
            ValidationService.ValidateModel(comment);

            UnitOfWork.ProfileComments.Add(comment);
            await UnitOfWork.Complete();
        }

        public async Task CreateCommentBank(ProfileCommentBank commentBank)
        {
            ValidationService.ValidateModel(commentBank);

            UnitOfWork.ProfileCommentBanks.Add(commentBank);
            await UnitOfWork.Complete();
        }

        public async Task CreateLog(ProfileLog log, int academicYearId, string userId)
        {
            using (var staffService = new StaffMemberService(UnitOfWork))
            {
                var author = await staffService.GetStaffMemberByUserId(userId);

                log.Date = DateTime.Now;
                log.AuthorId = author.Id;
                log.AcademicYearId = academicYearId;

                ValidationService.ValidateModel(log);

                UnitOfWork.ProfileLogs.Add(log);
                await UnitOfWork.Complete();
            }
        }

        public async Task DeleteComment(int commentId)
        {
            var comment = await GetCommentById(commentId);

            UnitOfWork.ProfileComments.Remove(comment);
            await UnitOfWork.Complete();
        }

        public async Task DeleteCommentBank(int commentBankId)
        {
            var commentBank = await UnitOfWork.ProfileCommentBanks.GetById(commentBankId);

            UnitOfWork.ProfileCommentBanks.Remove(commentBank);
            await UnitOfWork.Complete();
        }

        public async Task DeleteLog(int logId)
        {
            var logInDb = await GetLogById(logId);

            UnitOfWork.ProfileLogs.Remove(logInDb); //Delete from database

            await UnitOfWork.Complete();
        }

        public async Task<IEnumerable<ProfileCommentBank>> GetAllCommentBanks()
        {
            return await UnitOfWork.ProfileCommentBanks.GetAll();
        }

        public async Task<IDictionary<int, string>> GetAllCommentBanksLookup()
        {
            var commentBanks = await GetAllCommentBanks();

            return commentBanks.ToDictionary(x => x.Id, x => x.Name);
        }

        public async Task<IEnumerable<ProfileComment>> GetAllComments()
        {
            return await UnitOfWork.ProfileComments.GetAll();
        }

        public async Task<ProfileCommentBank> GetCommentBankById(int commentBankId)
        {
            var commentBankInDb = await UnitOfWork.ProfileCommentBanks.GetById(commentBankId);

            if (commentBankInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Comment bank not found");
            }

            return commentBankInDb;
        }

        public async Task<ProfileComment> GetCommentById(int commentId)
        {
            var comment = await UnitOfWork.ProfileComments.GetById(commentId);

            if (comment == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Comment not found");
            }

            return comment;
        }

        public async Task<IEnumerable<ProfileComment>> GetCommentsByBank(int commentBankId)
        {
            var comments = await UnitOfWork.ProfileComments.GetByCommentBank(commentBankId);

            return comments;
        }

        public async Task<ProfileLog> GetLogById(int logId)
        {
            var log = await UnitOfWork.ProfileLogs.GetById(logId);

            if (log == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Log not found");
            }

            return log;
        }

        public async Task<IEnumerable<ProfileLog>> GetLogsByStudent(int studentId, int academicYearId)
        {
            var logs = await UnitOfWork.ProfileLogs.GetByStudent(studentId, academicYearId);

            return logs;
        }

        public async Task UpdateComment(ProfileComment comment)
        {
            var commentInDb = await GetCommentById(comment.Id);

            commentInDb.Value = comment.Value;
            commentInDb.CommentBankId = comment.CommentBankId;

            await UnitOfWork.Complete();
        }

        public async Task UpdateCommentBank(ProfileCommentBank commentBank)
        {
            var commentBankInDb = await GetCommentBankById(commentBank.Id);
            
            commentBankInDb.Name = commentBank.Name;

            await UnitOfWork.Complete();
        }

        public async Task UpdateLog(ProfileLog log)
        {
            var logInDb = await GetLogById(log.Id);

            logInDb.TypeId = log.TypeId;
            logInDb.Message = log.Message;

            await UnitOfWork.Complete();
        }

        public async Task<IEnumerable<ProfileLogType>> GetAllLogTypes()
        {
            var logTypes = await UnitOfWork.ProfileLogTypes.GetAll();

            return logTypes;
        }

        public async Task<IDictionary<int, string>> GetAllLogTypesLookup()
        {
            var logTypes = await GetAllLogTypes();

            return logTypes.ToDictionary(x => x.Id, x => x.Name);
        }
    }
}