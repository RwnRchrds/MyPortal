using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class LogNoteService : BaseService, ILogNoteService
    {
        public LogNoteService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<LogNoteModel> GetById(Guid logNoteId)
        {
            var logNote = await UnitOfWork.LogNotes.GetById(logNoteId);

            if (logNote == null)
            {
                throw new NotFoundException("Log note not found.");
            }

            return BusinessMapper.Map<LogNoteModel>(logNote);
        }

        public async Task<IEnumerable<LogNoteModel>> GetByStudent(Guid studentId, Guid academicYearId)
        {
            var logNotes = await UnitOfWork.LogNotes.GetByStudent(studentId, academicYearId);

            return logNotes.OrderByDescending(n => n.CreatedDate).Select(BusinessMapper.Map<LogNoteModel>);
        }

        public async Task<IEnumerable<LogNoteTypeModel>> GetTypes()
        {
            var logNoteTypes = await UnitOfWork.LogNoteTypes.GetAll();

            return logNoteTypes.Select(BusinessMapper.Map<LogNoteTypeModel>);
        }

        public async Task Create(params LogNoteModel[] logNoteObjects)
        {
            foreach (var logNoteObject in logNoteObjects)
            {
                await AcademicYearModel.CheckLock(UnitOfWork.AcademicYears, logNoteObject.AcademicYearId);
                
                var createDate = DateTime.Now;

                var logNote = new LogNote
                {
                    TypeId = logNoteObject.TypeId,
                    Message = logNoteObject.Message,
                    StudentId = logNoteObject.StudentId,
                    CreatedDate = createDate,
                    UpdatedDate = createDate,
                    CreatedById = logNoteObject.CreatedById,
                    UpdatedById = logNoteObject.UpdatedById,
                    AcademicYearId = logNoteObject.AcademicYearId
                };

                UnitOfWork.LogNotes.Create(logNote);
            }

            await UnitOfWork.SaveChanges();
        }

        public async Task Update(params LogNoteModel[] logNoteObjects)
        {
            foreach (var logNoteObject in logNoteObjects)
            {
                await AcademicYearModel.CheckLock(UnitOfWork.AcademicYears, logNoteObject.AcademicYearId);

                var updateDate = DateTime.Now;

                var logNote = await UnitOfWork.LogNotes.GetByIdForEditing(logNoteObject.Id);

                if (logNote == null)
                {
                    throw new NotFoundException("Log note not found.");
                }
                
                await AcademicYearModel.CheckLock(UnitOfWork.AcademicYears, logNote.AcademicYearId);

                logNote.TypeId = logNoteObject.TypeId;
                logNote.Message = logNoteObject.Message;
                logNote.UpdatedDate = updateDate;
                logNote.UpdatedById = logNoteObject.UpdatedById;
            }

            await UnitOfWork.SaveChanges();
        }

        public async Task Delete(params Guid[] logNoteIds)
        {
            foreach (var logNoteId in logNoteIds)
            {
                var logNote = await GetById(logNoteId);

                await AcademicYearModel.CheckLock(UnitOfWork.AcademicYears, logNote.AcademicYearId);
                
                await UnitOfWork.LogNotes.Delete(logNoteId);
            }

            await UnitOfWork.SaveChanges();
        }

        public override void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
