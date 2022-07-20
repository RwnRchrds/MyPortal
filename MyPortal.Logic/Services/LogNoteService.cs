using System;
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
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Student.LogNotes;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class LogNoteService : BaseService, ILogNoteService
    {
        public async Task<LogNoteModel> GetLogNoteById(Guid logNoteId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var logNote = await unitOfWork.LogNotes.GetById(logNoteId);

                if (logNote == null)
                {
                    throw new NotFoundException("Log note not found.");
                }

                return new LogNoteModel(logNote);
            }
        }

        public async Task<IEnumerable<LogNoteModel>> GetLogNotesByStudent(Guid studentId, Guid academicYearId, bool includeRestricted)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var logNotes = await unitOfWork.LogNotes.GetByStudent(studentId, academicYearId, includeRestricted);

                return logNotes.OrderByDescending(n => n.CreatedDate).Select(l => new LogNoteModel(l)).ToList();
            }
        }

        public async Task<IEnumerable<LogNoteTypeModel>> GetLogNoteTypes()
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var logNoteTypes = await unitOfWork.LogNoteTypes.GetAll();

                return logNoteTypes.Select(t => new LogNoteTypeModel(t));
            }
        }

        public async Task CreateLogNote(params CreateLogNoteRequestModel[] logNoteObjects)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var logNoteObject in logNoteObjects)
                {
                    await AcademicHelper.IsAcademicYearLocked(logNoteObject.AcademicYearId, true);

                    var createDate = DateTime.Now;

                    var logNote = new LogNote
                    {
                        TypeId = logNoteObject.TypeId,
                        Message = logNoteObject.Message,
                        StudentId = logNoteObject.StudentId,
                        CreatedDate = createDate,
                        CreatedById = logNoteObject.CreatedById,
                        AcademicYearId = logNoteObject.AcademicYearId
                    };

                    unitOfWork.LogNotes.Create(logNote);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdateLogNote(params UpdateLogNoteRequestModel[] logNoteObjects)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var logNoteObject in logNoteObjects)
                {
                    var logNote = await unitOfWork.LogNotes.GetById(logNoteObject.Id);
                    
                    await AcademicHelper.IsAcademicYearLocked(logNote.AcademicYearId, true);

                    var updateDate = DateTime.Now;

                    if (logNote == null)
                    {
                        throw new NotFoundException("Log note not found.");
                    }

                    logNote.TypeId = logNoteObject.TypeId;
                    logNote.Message = logNoteObject.Message;

                    await unitOfWork.LogNotes.Update(logNote);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteLogNote(params Guid[] logNoteIds)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var logNoteId in logNoteIds)
                {
                    var logNote = await GetLogNoteById(logNoteId);

                    await AcademicHelper.IsAcademicYearLocked(logNote.AcademicYearId, true);

                    await unitOfWork.LogNotes.Delete(logNoteId);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }
    }
}
