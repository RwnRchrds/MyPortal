using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.BusinessLogic.Exceptions;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.BusinessLogic.Services
{
    public class ProfilesService : MyPortalService
    {
        public ProfilesService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            
        }

        public ProfilesService() : base()
        {

        }

        public async Task CreateComment(Comment comment)
        {
            ValidationService.ValidateModel(comment);

            UnitOfWork.Comments.Add(comment);
            await UnitOfWork.Complete();
        }

        public async Task CreateCommentBank(CommentBank commentBank)
        {
            ValidationService.ValidateModel(commentBank);

            UnitOfWork.CommentBanks.Add(commentBank);
            await UnitOfWork.Complete();
        }

        public async Task CreateLog(ProfileLogNote logNote, int academicYearId, string userId)
        {
            using (var staffService = new StaffMemberService())
            {
                var author = await staffService.GetStaffMemberByUserId(userId);

                logNote.Date = DateTime.Now;
                logNote.AuthorId = author.Id;
                logNote.AcademicYearId = academicYearId;

                ValidationService.ValidateModel(logNote);

                UnitOfWork.ProfileLogNotes.Add(logNote);
                await UnitOfWork.Complete();
            }
        }

        public async Task DeleteComment(int commentId)
        {
            var comment = await GetCommentById(commentId);

            UnitOfWork.Comments.Remove(comment);
            await UnitOfWork.Complete();
        }

        public async Task DeleteCommentBank(int commentBankId)
        {
            var commentBank = await UnitOfWork.CommentBanks.GetById(commentBankId);

            UnitOfWork.CommentBanks.Remove(commentBank);
            await UnitOfWork.Complete();
        }

        public async Task DeleteLog(int logId)
        {
            var logInDb = await GetLogById(logId);

            UnitOfWork.ProfileLogNotes.Remove(logInDb); //Delete from database

            await UnitOfWork.Complete();
        }

        public async Task<IEnumerable<CommentBank>> GetAllCommentBanks()
        {
            return await UnitOfWork.CommentBanks.GetAll();
        }

        public async Task<IDictionary<int, string>> GetAllCommentBanksLookup()
        {
            var commentBanks = await GetAllCommentBanks();

            return commentBanks.ToDictionary(x => x.Id, x => x.Name);
        }

        public async Task<IEnumerable<Comment>> GetAllComments()
        {
            return await UnitOfWork.Comments.GetAll();
        }

        public async Task<CommentBank> GetCommentBankById(int commentBankId)
        {
            var commentBankInDb = await UnitOfWork.CommentBanks.GetById(commentBankId);

            if (commentBankInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Comment bank not found");
            }

            return commentBankInDb;
        }

        public async Task<Comment> GetCommentById(int commentId)
        {
            var comment = await UnitOfWork.Comments.GetById(commentId);

            if (comment == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Comment not found");
            }

            return comment;
        }

        public async Task<IEnumerable<Comment>> GetCommentsByBank(int commentBankId)
        {
            var comments = await UnitOfWork.Comments.GetByCommentBank(commentBankId);

            return comments;
        }

        public async Task<ProfileLogNote> GetLogById(int logId)
        {
            var log = await UnitOfWork.ProfileLogNotes.GetById(logId);

            if (log == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Log not found");
            }

            return log;
        }

        public async Task<IEnumerable<ProfileLogNote>> GetLogsByStudent(int studentId, int academicYearId)
        {
            var logs = await UnitOfWork.ProfileLogNotes.GetByStudent(studentId, academicYearId);

            return logs;
        }

        public async Task UpdateComment(Comment comment)
        {
            var commentInDb = await GetCommentById(comment.Id);

            commentInDb.Value = comment.Value;
            commentInDb.CommentBankId = comment.CommentBankId;

            await UnitOfWork.Complete();
        }

        public async Task UpdateCommentBank(CommentBank commentBank)
        {
            var commentBankInDb = await GetCommentBankById(commentBank.Id);
            
            commentBankInDb.Name = commentBank.Name;

            await UnitOfWork.Complete();
        }

        public async Task UpdateLog(ProfileLogNote logNote)
        {
            var logInDb = await GetLogById(logNote.Id);

            logInDb.TypeId = logNote.TypeId;
            logInDb.Message = logNote.Message;

            await UnitOfWork.Complete();
        }

        public async Task<IEnumerable<ProfileLogNoteType>> GetAllLogTypes()
        {
            var logTypes = await UnitOfWork.ProfileLogNoteTypes.GetAll();

            return logTypes;
        }

        public async Task<IDictionary<int, string>> GetAllLogTypesLookup()
        {
            var logTypes = await GetAllLogTypes();

            return logTypes.ToDictionary(x => x.Id, x => x.Name);
        }
    }
}