using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Details;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class ProfileLogNoteService : BaseService, IProfileLogNoteService
    {
        private readonly IProfileLogNoteRepository _repository;
        private readonly IProfileLogNoteTypeRepository _typeRepository;

        public ProfileLogNoteService(IProfileLogNoteRepository repository, IProfileLogNoteTypeRepository typeRepository)
        {
            _repository = repository;
            _typeRepository = typeRepository;
        }

        public async Task<ProfileLogNoteDetails> GetById(Guid logNoteId)
        {
            var logNote = await _repository.GetById(logNoteId);

            return _businessMapper.Map<ProfileLogNoteDetails>(logNote);
        }

        public async Task<IEnumerable<ProfileLogNoteDetails>> GetByStudent(Guid studentId, Guid academicYearId)
        {
            var logNotes = await _repository.GetByStudent(studentId, academicYearId);

            return logNotes.Select(_businessMapper.Map<ProfileLogNoteDetails>);
        }

        public async Task<Lookup> GetTypes()
        {
            var logNoteTypes = (await _typeRepository.GetAll()).ToDictionary(x => x.Name, x => x.Id);

            return new Lookup(logNoteTypes);
        }

        public async Task Create(params ProfileLogNoteDetails[] logNoteObjects)
        {
            foreach (var logNoteObject in logNoteObjects)
            {
                var logNote = new ProfileLogNote
                {
                    TypeId = logNoteObject.TypeId,
                    Message = logNoteObject.Message,
                    StudentId = logNoteObject.StudentId,
                    Date = DateTime.Now,
                    AuthorId = logNoteObject.AuthorId,
                    AcademicYearId = logNoteObject.AcademicYearId
                };

                _repository.Create(logNote);
            }

            await _repository.SaveChanges();
        }

        public async Task Update(params ProfileLogNoteDetails[] logNoteObjects)
        {
            foreach (var logNoteObject in logNoteObjects)
            {
                var logNote = await _repository.GetByIdWithTracking(logNoteObject.Id);

                logNote.TypeId = logNoteObject.TypeId;
                logNote.Message = logNoteObject.Message;
            }

            await _repository.SaveChanges();
        }

        public async Task Delete(params Guid[] logNoteIds)
        {
            foreach (var logNoteId in logNoteIds)
            {
                await _repository.Delete(logNoteId);
            }

            await _repository.SaveChanges();
        }
    }
}
