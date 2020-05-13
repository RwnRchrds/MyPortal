using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Business;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Exceptions;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class LogNoteService : BaseService, ILogNoteService
    {
        private readonly ILogNoteRepository _logNoteRepository;
        private readonly ILogNoteTypeRepository _logNoteTypeRepository;

        public LogNoteService(ILogNoteRepository logNoteRepository, ILogNoteTypeRepository logNoteTypeRepository) : base("Log note")
        {
            _logNoteRepository = logNoteRepository;
            _logNoteTypeRepository = logNoteTypeRepository;
        }

        public async Task<LogNoteModel> GetById(Guid logNoteId)
        {
            var logNote = await _logNoteRepository.GetById(logNoteId);

            if (logNote == null)
            {
                NotFound();
            }

            return _businessMapper.Map<LogNoteModel>(logNote);
        }

        public async Task<IEnumerable<LogNoteModel>> GetByStudent(Guid studentId, Guid academicYearId)
        {
            var logNotes = await _logNoteRepository.GetByStudent(studentId, academicYearId);

            return logNotes.Select(_businessMapper.Map<LogNoteModel>);
        }

        public async Task<Lookup> GetTypes()
        {
            var logNoteTypes = (await _logNoteTypeRepository.GetAll()).ToDictionary(x => x.Name, x => x.Id);

            return new Lookup(logNoteTypes);
        }

        public async Task Create(params LogNoteModel[] logNoteObjects)
        {
            foreach (var logNoteObject in logNoteObjects)
            {
                var logNote = new LogNote
                {
                    TypeId = logNoteObject.TypeId,
                    Message = logNoteObject.Message,
                    StudentId = logNoteObject.StudentId,
                    Date = DateTime.Now,
                    AuthorId = logNoteObject.AuthorId,
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
                var logNote = await _logNoteRepository.GetByIdWithTracking(logNoteObject.Id);

                if (logNote == null)
                {
                    NotFound();
                }

                logNote.TypeId = logNoteObject.TypeId;
                logNote.Message = logNoteObject.Message;
            }

            await _logNoteRepository.SaveChanges();
        }

        public async Task Delete(params Guid[] logNoteIds)
        {
            foreach (var logNoteId in logNoteIds)
            {
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
