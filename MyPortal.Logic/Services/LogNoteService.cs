using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<LogNoteModel> GetById(Guid logNoteId)
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

        public async Task<IEnumerable<LogNoteModel>> GetByStudent(Guid studentId, Guid academicYearId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var logNotes = await unitOfWork.LogNotes.GetByStudent(studentId, academicYearId);

                return logNotes.OrderByDescending(n => n.CreatedDate).Select(l => new LogNoteModel(l)).ToList();
            }
        }

        public async Task<IEnumerable<LogNoteTypeModel>> GetTypes()
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var logNoteTypes = await unitOfWork.LogNoteTypes.GetAll();

                return logNoteTypes.Select(t => new LogNoteTypeModel(t));
            }
        }

        public async Task Create(params CreateLogNoteModel[] logNoteObjects)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var logNoteObject in logNoteObjects)
                {
                    await AcademicYearModel.CheckLock(unitOfWork, logNoteObject.AcademicYearId);

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

        public async Task Update(params UpdateLogNoteModel[] logNoteObjects)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var logNoteObject in logNoteObjects)
                {
                    var logNote = await unitOfWork.LogNotes.GetById(logNoteObject.Id);
                    
                    await AcademicYearModel.CheckLock(unitOfWork, logNote.AcademicYearId);

                    var updateDate = DateTime.Now;

                    if (logNote == null)
                    {
                        throw new NotFoundException("Log note not found.");
                    }

                    await AcademicYearModel.CheckLock(unitOfWork, logNote.AcademicYearId);

                    logNote.TypeId = logNoteObject.TypeId;
                    logNote.Message = logNoteObject.Message;

                    await unitOfWork.LogNotes.Update(logNote);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task Delete(params Guid[] logNoteIds)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var logNoteId in logNoteIds)
                {
                    var logNote = await GetById(logNoteId);

                    await AcademicYearModel.CheckLock(unitOfWork, logNote.AcademicYearId);

                    await unitOfWork.LogNotes.Delete(logNoteId);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }
    }
}
