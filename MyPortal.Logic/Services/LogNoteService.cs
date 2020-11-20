using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Entity;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class LogNoteService : BaseService, ILogNoteService
    {
        private readonly IAcademicYearRepository _academicYearRepository;
        private readonly ILogNoteRepository _logNoteRepository;
        private readonly ILogNoteTypeRepository _logNoteTypeRepository;

        public LogNoteService(ApplicationDbContext context)
        {
            var connection = context.Database.GetDbConnection();

            _academicYearRepository = new AcademicYearRepository(context);
            _logNoteRepository = new LogNoteRepository(context);
            _logNoteTypeRepository = new LogNoteTypeRepository(connection);
        }

        public async Task<LogNoteModel> GetById(Guid logNoteId)
        {
            var logNote = await _logNoteRepository.GetById(logNoteId);

            if (logNote == null)
            {
                throw new NotFoundException("Log note not found.");
            }

            return BusinessMapper.Map<LogNoteModel>(logNote);
        }

        public async Task<IEnumerable<LogNoteModel>> GetByStudent(Guid studentId, Guid academicYearId)
        {
            var logNotes = await _logNoteRepository.GetByStudent(studentId, academicYearId);

            return logNotes.Select(BusinessMapper.Map<LogNoteModel>);
        }

        public async Task<IEnumerable<LogNoteTypeModel>> GetTypes()
        {
            var logNoteTypes = await _logNoteTypeRepository.GetAll();

            return logNoteTypes.Select(BusinessMapper.Map<LogNoteTypeModel>);
        }

        public async Task Create(params LogNoteModel[] logNoteObjects)
        {
            Guid? academicYearId = null;
            
            foreach (var logNoteObject in logNoteObjects)
            {
                await AcademicYearModel.CheckLock(_academicYearRepository, logNoteObject.AcademicYearId);
                
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

                _logNoteRepository.Create(logNote);
            }

            await _logNoteRepository.SaveChanges();
        }

        public async Task Update(params LogNoteModel[] logNoteObjects)
        {
            foreach (var logNoteObject in logNoteObjects)
            {
                var updateDate = DateTime.Now;

                var logNote = await _logNoteRepository.GetByIdWithTracking(logNoteObject.Id);

                if (logNote == null)
                {
                    throw new NotFoundException("Log note not found.");
                }
                
                await AcademicYearModel.CheckLock(_academicYearRepository, logNote.AcademicYearId);

                logNote.TypeId = logNoteObject.TypeId;
                logNote.Message = logNoteObject.Message;
                logNote.UpdatedDate = updateDate;
                logNote.UpdatedById = logNoteObject.UpdatedById;
            }

            await _logNoteRepository.SaveChanges();
        }

        public async Task Delete(params Guid[] logNoteIds)
        {
            foreach (var logNoteId in logNoteIds)
            {
                var logNote = await GetById(logNoteId);

                await AcademicYearModel.CheckLock(_academicYearRepository, logNote.AcademicYearId);
                
                await _logNoteRepository.Delete(logNoteId);
            }

            await _logNoteRepository.SaveChanges();
        }

        public override void Dispose()
        {
            _logNoteRepository.Dispose();
            _logNoteTypeRepository.Dispose();
        }
    }
}
