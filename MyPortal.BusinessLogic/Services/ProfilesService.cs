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
    public class ProfilesService : MyPortalService
    {
        public ProfilesService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            
        }

        public ProfilesService() : base()
        {

        }

        public async Task CreateComment(CommentDto comment)
        {
            ValidationService.ValidateModel(comment);

            UnitOfWork.Comments.Add(Mapper.Map<Comment>(comment));
            
        }

        public async Task CreateCommentBank(CommentBankDto commentBank)
        {
            ValidationService.ValidateModel(commentBank);

            UnitOfWork.CommentBanks.Add(Mapper.Map<CommentBank>(commentBank));
            
        }

        public async Task CreateLogNote(ProfileLogNoteDto logNote)
        {
            logNote.Date = DateTime.Now;

            ValidationService.ValidateModel(logNote);

            UnitOfWork.ProfileLogNotes.Add(Mapper.Map<ProfileLogNote>(logNote));
            
        }

        public async Task DeleteComment(int commentId)
        {
            var comment = await UnitOfWork.Comments.GetById(commentId);

            if (comment == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Comment not found.");
            }

            UnitOfWork.Comments.Remove(comment);
            
        }

        public async Task DeleteCommentBank(int commentBankId)
        {
            var commentBank = await UnitOfWork.CommentBanks.GetById(commentBankId);

            if (commentBank == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Comment bank not found.");
            }

            UnitOfWork.CommentBanks.Remove(commentBank);
            
        }

        public async Task DeleteLogNote(int logId)
        {
            var logInDb = await UnitOfWork.ProfileLogNotes.GetById(logId);

            if (logInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Log note not found.");
            }

            UnitOfWork.ProfileLogNotes.Remove(logInDb); //Delete from database

            
        }

        public async Task<IEnumerable<CommentBankDto>> GetAllCommentBanks()
        {
            return (await UnitOfWork.CommentBanks.GetAll()).Select(Mapper.Map<CommentBankDto>).ToList();
        }

        public async Task<IDictionary<int, string>> GetAllCommentBanksLookup()
        {
            return (await GetAllCommentBanks()).ToDictionary(x => x.Id, x => x.Name);
        }

        public async Task<IEnumerable<CommentDto>> GetAllComments()
        {
            return (await UnitOfWork.Comments.GetAll()).Select(Mapper.Map<CommentDto>).ToList();
        }

        public async Task<IEnumerable<ProfileLogNoteTypeDto>> GetAllLogNoteTypes()
        {
            return (await UnitOfWork.ProfileLogNoteTypes.GetAll()).Select(Mapper.Map<ProfileLogNoteTypeDto>).ToList();
        }

        public async Task<IDictionary<int, string>> GetAllLogTypesLookup()
        {
            return (await GetAllLogNoteTypes()).ToDictionary(x => x.Id, x => x.Name);
        }

        public async Task<CommentBankDto> GetCommentBankById(int commentBankId)
        {
            var commentBankInDb = await UnitOfWork.CommentBanks.GetById(commentBankId);

            if (commentBankInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Comment bank not found");
            }

            return Mapper.Map<CommentBankDto>(commentBankInDb);
        }

        public async Task<CommentDto> GetCommentById(int commentId)
        {
            var comment = await UnitOfWork.Comments.GetById(commentId);

            if (comment == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Comment not found");
            }

            return Mapper.Map<CommentDto>(comment);
        }

        public async Task<IEnumerable<CommentDto>> GetCommentsByBank(int commentBankId)
        {
            return (await UnitOfWork.Comments.GetByCommentBank(commentBankId)).Select(Mapper.Map<CommentDto>).ToList();
        }

        public async Task<ProfileLogNoteDto> GetLogNoteById(int logId)
        {
            var log = await UnitOfWork.ProfileLogNotes.GetById(logId);

            if (log == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Log not found");
            }

            return Mapper.Map<ProfileLogNoteDto>(log);
        }

        public async Task<IEnumerable<ProfileLogNoteDto>> GetLogNotesByStudent(int studentId, int academicYearId)
        {
            return (await UnitOfWork.ProfileLogNotes.GetByStudent(studentId, academicYearId)).Select(
                Mapper.Map<ProfileLogNoteDto>).ToList();
        }

        public async Task UpdateComment(CommentDto comment)
        {
            var commentInDb = await UnitOfWork.Comments.GetById(comment.Id);

            commentInDb.Value = comment.Value;
            commentInDb.CommentBankId = comment.CommentBankId;

            
        }

        public async Task UpdateCommentBank(CommentBankDto commentBank)
        {
            var commentBankInDb = await UnitOfWork.CommentBanks.GetById(commentBank.Id);
            
            commentBankInDb.Name = commentBank.Name;

            
        }

        public async Task UpdateLogNote(ProfileLogNoteDto logNote)
        {
            var logInDb = await UnitOfWork.ProfileLogNotes.GetById(logNote.Id);

            logInDb.TypeId = logNote.TypeId;
            logInDb.Message = logNote.Message;

            
        }
    }
}