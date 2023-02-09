﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MyPortal.Database;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.Students;

using MyPortal.Logic.Models.Requests.Student.LogNotes;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class LogNoteService : BaseUserService, ILogNoteService
    {
        public LogNoteService(ICurrentUser user) : base(user)
        {
        }

        public async Task<LogNoteModel> GetLogNoteById(Guid logNoteId)
        {
            await using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var logNote = await unitOfWork.LogNotes.GetById(logNoteId);

                if (logNote == null)
                {
                    throw new NotFoundException("Log note not found.");
                }

                return new LogNoteModel(logNote);
            }
        }

        public async Task<IEnumerable<LogNoteModel>> GetLogNotesByStudent(Guid studentId, Guid academicYearId, bool includePrivate)
        {
            await using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var logNotes = await unitOfWork.LogNotes.GetByStudent(studentId, academicYearId, includePrivate);

                return logNotes.OrderByDescending(n => n.CreatedDate).Select(l => new LogNoteModel(l)).ToList();
            }
        }

        public async Task<IEnumerable<LogNoteTypeModel>> GetLogNoteTypes()
        {
            await using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var logNoteTypes = await unitOfWork.LogNoteTypes.GetAll();

                return logNoteTypes.Select(t => new LogNoteTypeModel(t));
            }
        }

        public async Task CreateLogNote(LogNoteRequestModel logNoteModel)
        {
            Validate(logNoteModel);
            
            await using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                await AcademicHelper.IsAcademicYearLocked(logNoteModel.AcademicYearId, true);

                var createDate = DateTime.Now;

                var logNote = new LogNote
                {
                    TypeId = logNoteModel.TypeId,
                    Message = logNoteModel.Message,
                    StudentId = logNoteModel.StudentId,
                    CreatedDate = createDate,
                    CreatedById = User.GetUserId(),
                    AcademicYearId = logNoteModel.AcademicYearId
                };

                unitOfWork.LogNotes.Create(logNote);

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdateLogNote(Guid logNoteId, LogNoteRequestModel logNoteModel)
        {
            Validate(logNoteModel);
            
            await using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var logNote = await unitOfWork.LogNotes.GetById(logNoteId);
                    
                await AcademicHelper.IsAcademicYearLocked(logNote.AcademicYearId, true);

                if (logNote == null)
                {
                    throw new NotFoundException("Log note not found.");
                }

                logNote.TypeId = logNoteModel.TypeId;
                logNote.Message = logNoteModel.Message;

                await unitOfWork.LogNotes.Update(logNote);

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteLogNote(Guid logNoteId)
        {
            await using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var logNote = await GetLogNoteById(logNoteId);

                await AcademicHelper.IsAcademicYearLocked(logNote.AcademicYearId, true);

                await unitOfWork.LogNotes.Delete(logNoteId);

                await unitOfWork.SaveChangesAsync();
            }
        }
    }
}
