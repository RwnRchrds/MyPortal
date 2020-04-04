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
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class ProfileLogNoteService : BaseService, IProfileLogNoteService
    {
        private readonly IProfileLogNoteRepository _profileLogNoteRepository;
        private readonly IProfileLogNoteTypeRepository _profileLogNoteTypeRepository;

        public ProfileLogNoteService(IProfileLogNoteRepository profileLogNoteRepository, IProfileLogNoteTypeRepository profileLogNoteTypeRepository)
        {
            _profileLogNoteRepository = profileLogNoteRepository;
            _profileLogNoteTypeRepository = profileLogNoteTypeRepository;
        }

        public async Task<ProfileLogNoteModel> GetById(Guid logNoteId)
        {
            var logNote = await _profileLogNoteRepository.GetById(logNoteId);

            return _businessMapper.Map<ProfileLogNoteModel>(logNote);
        }

        public async Task<IEnumerable<ProfileLogNoteModel>> GetByStudent(Guid studentId, Guid academicYearId)
        {
            var logNotes = await _profileLogNoteRepository.GetByStudent(studentId, academicYearId);

            return logNotes.Select(_businessMapper.Map<ProfileLogNoteModel>);
        }

        public async Task<Lookup> GetTypes()
        {
            var logNoteTypes = (await _profileLogNoteTypeRepository.GetAll()).ToDictionary(x => x.Name, x => x.Id);

            return new Lookup(logNoteTypes);
        }

        public async Task Create(params ProfileLogNoteModel[] logNoteObjects)
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

                _profileLogNoteRepository.Create(logNote);
            }

            await _profileLogNoteRepository.SaveChanges();
        }

        public async Task Update(params ProfileLogNoteModel[] logNoteObjects)
        {
            foreach (var logNoteObject in logNoteObjects)
            {
                var logNote = await _profileLogNoteRepository.GetByIdWithTracking(logNoteObject.Id);

                logNote.TypeId = logNoteObject.TypeId;
                logNote.Message = logNoteObject.Message;
            }

            await _profileLogNoteRepository.SaveChanges();
        }

        public async Task Delete(params Guid[] logNoteIds)
        {
            foreach (var logNoteId in logNoteIds)
            {
                await _profileLogNoteRepository.Delete(logNoteId);
            }

            await _profileLogNoteRepository.SaveChanges();
        }
    }
}
