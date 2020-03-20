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

        public async Task<IEnumerable<ProfileLogNoteDetails>> GetByStudent(Guid studentId)
        {
            var logNotes = await _repository.GetByStudent(studentId);

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
                    
                };

                _repository.Create(logNote);
            }

            await _repository.SaveChanges();
        }

        public async Task Update(params ProfileLogNoteDetails[] logNoteObjects)
        {
            foreach (var logNoteObject in logNoteObjects)
            {
                var logNote = _businessMapper.Map<ProfileLogNote>(logNoteObject);

                await _repository.Update(logNote);
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
