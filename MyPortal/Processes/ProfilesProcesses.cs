using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Microsoft.Owin.Security.Provider;
using MyPortal.Dtos;
using MyPortal.Dtos.GridDtos;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;

namespace MyPortal.Processes
{
    public static class ProfilesProcesses
    {
        public static ProcessResponse<object> CreateLog(ProfileLog log, int academicYearId, string userId, MyPortalDbContext context)
        {
            var authorId = log.AuthorId;

            var author = PeopleProcesses.HandleAuthorFromUserId(userId, authorId, context).ResponseObject;

            log.Date = DateTime.Now;
            log.AuthorId = author.Id;
            log.AcademicYearId = academicYearId;

            if (!ValidationProcesses.ModelIsValid(log))
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Invalid data", null);
            }

            context.ProfileLogs.Add(log);
            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Log note created", null);
        }

        public static ProcessResponse<object> DeleteLog(int logId, MyPortalDbContext context)
        {
            var logInDb = context.ProfileLogs.SingleOrDefault(l => l.Id == logId);

            if (logInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Log note not found", null);
            }

            logInDb.Deleted = true;
            //_context.ProfileLogs.Remove(logInDb); //Delete from database
            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Log note deleted", null);
        }

        public static ProcessResponse<ProfileLogDto> GetLogById(int logId, MyPortalDbContext context)
        {
            var log = context.ProfileLogs.SingleOrDefault(l => l.Id == logId);

            if (log == null)
            {
                return new ProcessResponse<ProfileLogDto>(ResponseType.NotFound, "Log note not found", null);
            }

            return new ProcessResponse<ProfileLogDto>(ResponseType.Ok, null,
                Mapper.Map<ProfileLog, ProfileLogDto>(log));
        }

        public static ProcessResponse<IEnumerable<ProfileLogDto>> GetLogsForStudent(int studentId,
            int academicYearId, MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<ProfileLogDto>>(ResponseType.Ok, null, context.ProfileLogs
                .Where(l => l.StudentId == studentId && l.AcademicYearId == academicYearId)
                .OrderByDescending(x => x.Date)
                .ToList()
                .Select(Mapper.Map<ProfileLog, ProfileLogDto>));
        }

        public static ProcessResponse<IEnumerable<GridProfileLogDto>> GetLogsForStudent_DataGrid(int studentId,
            int academicYearId, MyPortalDbContext context)
        {
            var logs = context.ProfileLogs
                .Where(x => x.AcademicYearId == academicYearId && x.StudentId == studentId && !x.Deleted)
                .OrderByDescending(x => x.Date).ToList().Select(Mapper.Map<ProfileLog, GridProfileLogDto>);

            return new ProcessResponse<IEnumerable<GridProfileLogDto>>(ResponseType.Ok, null, logs);
        }

        public static ProcessResponse<object> UpdateLog(ProfileLog log, MyPortalDbContext context)
        {
            if (!ValidationProcesses.ModelIsValid(log))
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Invalid data", null);
            }

            var logInDb = context.ProfileLogs.SingleOrDefault(l => l.Id == log.Id);

            if (logInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Log note not found", null);
            }

            logInDb.TypeId = log.TypeId;
            logInDb.Message = log.Message;

            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Log note updated", null);
        }

        public static ProcessResponse<bool> CommentBankContainsComments(int bankId, MyPortalDbContext context)
        {
            var commentBank = context.ProfileCommentBanks.SingleOrDefault(x => x.Id == bankId);

            if (commentBank == null)
            {
                return new ProcessResponse<bool>(ResponseType.NotFound, "Comment bank not found", false);
            }

            return new ProcessResponse<bool>(ResponseType.Ok, null, commentBank.ProfileComments.Any());
        }

        public static ProcessResponse<object> CreateCommentBank(ProfileCommentBank commentBank,
            MyPortalDbContext context)
        {
            if (ValidationProcesses.ModelIsValid(commentBank) || string.IsNullOrWhiteSpace(commentBank.Name))
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Invalid data", null);
            }


            if (context.ProfileCommentBanks.Any(x => x.Name == commentBank.Name))
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Comment bank already exists", null);
            }

            context.ProfileCommentBanks.Add(commentBank);
            context.SaveChanges();
            return new ProcessResponse<object>(ResponseType.Ok, "Comment bank created", null);
        }

        public static ProcessResponse<object> DeleteCommentBank(int commentBankId, MyPortalDbContext context)
        {
            var commentBank = context.ProfileCommentBanks.SingleOrDefault(x => x.Id == commentBankId);

            if (commentBank == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Comment bank not found", null);
            }

            var comments = context.ProfileComments.Where(x => x.CommentBankId == commentBankId);

            if (comments.Any())
            {
                context.ProfileComments.RemoveRange(comments);
            }

            context.ProfileCommentBanks.Remove(commentBank);
            context.SaveChanges();
            return new ProcessResponse<object>(ResponseType.Ok, "Comment bank deleted", null);
        }

        public static ProcessResponse<ProfileCommentBankDto> GetCommentBankById(int commentBankId,
            MyPortalDbContext context)
        {
            var commentBankInDb = context.ProfileCommentBanks.SingleOrDefault(x => x.Id == commentBankId);

            if (commentBankInDb == null)
            {
                return new ProcessResponse<ProfileCommentBankDto>(ResponseType.NotFound, "Comment bank not found", null);
            }

            return new ProcessResponse<ProfileCommentBankDto>(ResponseType.Ok, null,
                Mapper.Map<ProfileCommentBank, ProfileCommentBankDto>(commentBankInDb));
        }
        
        public static ProcessResponse<IEnumerable<ProfileCommentBank>> GetAllCommentBanks_Model(MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<ProfileCommentBank>>(ResponseType.Ok, null,
                context.ProfileCommentBanks.OrderBy(x => x.Name).ToList());
        }

        public static ProcessResponse<IEnumerable<ProfileCommentBankDto>> GetAllCommentBanks(MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<ProfileCommentBankDto>>(ResponseType.Ok, null,
                GetAllCommentBanks_Model(context).ResponseObject
                    .Select(Mapper.Map<ProfileCommentBank, ProfileCommentBankDto>));
        }
        
        public static ProcessResponse<IEnumerable<GridProfileCommentBankDto>> GetAllCommentBanks_DataGrid(MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<GridProfileCommentBankDto>>(ResponseType.Ok, null,
                GetAllCommentBanks_Model(context).ResponseObject
                    .Select(Mapper.Map<ProfileCommentBank, GridProfileCommentBankDto>));
        }

        public static ProcessResponse<object> UpdateCommentBank(ProfileCommentBank commentBank,
            MyPortalDbContext context)
        {
            var commentBankInDb = context.ProfileCommentBanks.SingleOrDefault(x => x.Id == commentBank.Id);

            if (commentBankInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Comment bank not found", null);
            }

            commentBankInDb.Name = commentBank.Name;

            context.SaveChanges();
            return new ProcessResponse<object>(ResponseType.Ok, "Comment bank updated", null);
        }

        public static ProcessResponse<object> CreateComment(ProfileComment comment, MyPortalDbContext context)
        {
            if (!ValidationProcesses.ModelIsValid(comment))
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Invalid data", null);
            }

            context.ProfileComments.Add(comment);
            context.SaveChanges();
            return new ProcessResponse<object>(ResponseType.Ok, "Comment created", null);
        }

        public static ProcessResponse<object> DeleteComment(int commentId, MyPortalDbContext context)
        {
            var comment = context.ProfileComments.SingleOrDefault(x => x.Id == commentId);

            if (comment == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Comment not found", null);
            }

            context.ProfileComments.Remove(comment);
            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Comment deleted", null);
        }

        public static ProcessResponse<ProfileCommentDto> GetCommentById(int commentId, MyPortalDbContext context)
        {
            var comment = context.ProfileComments.SingleOrDefault(x => x.Id == commentId);

            if (comment == null)
            {
                return new ProcessResponse<ProfileCommentDto>(ResponseType.NotFound, "Comment not found", null);
            }

            return new ProcessResponse<ProfileCommentDto>(ResponseType.Ok, null,
                Mapper.Map<ProfileComment, ProfileCommentDto>(comment));
        }

        public static ProcessResponse<IEnumerable<ProfileCommentDto>> GetAllComments(MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<ProfileCommentDto>>(ResponseType.Ok, null, context.ProfileComments
                .OrderBy(x => x.Value)
                .ToList()
                .Select(Mapper.Map<ProfileComment, ProfileCommentDto>));
        }
        
        public static ProcessResponse<IEnumerable<ProfileComment>> GetCommentsByBank_Model(int commentBankId,
            MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<ProfileComment>>(ResponseType.Ok, null, context.ProfileComments
                .Where(x => x.CommentBankId == commentBankId)
                .OrderBy(x => x.Value)
                .ToList());
        }

        public static ProcessResponse<IEnumerable<ProfileCommentDto>> GetCommentsByBank(int commentBankId,
            MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<ProfileCommentDto>>(ResponseType.Ok, null,
                GetCommentsByBank_Model(commentBankId, context).ResponseObject
                    .Select(Mapper.Map<ProfileComment, ProfileCommentDto>));
        }
        
        public static ProcessResponse<IEnumerable<GridProfileCommentDto>> GetCommentsByBank_DataGrid(int commentBankId,
            MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<GridProfileCommentDto>>(ResponseType.Ok, null,
                GetCommentsByBank_Model(commentBankId, context).ResponseObject
                    .Select(Mapper.Map<ProfileComment, GridProfileCommentDto>));
        }

        public static ProcessResponse<object> UpdateComment(ProfileComment comment, MyPortalDbContext context)
        {
            if (!ValidationProcesses.ModelIsValid(comment))
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Invalid data", null);
            }

            var commentInDb = context.ProfileComments.SingleOrDefault(x => x.Id == comment.Id);

            if (commentInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Comment not found", null);
            }

            commentInDb.Value = comment.Value;
            commentInDb.CommentBankId = comment.CommentBankId;

            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Comment updated", null);
        }
    }
}