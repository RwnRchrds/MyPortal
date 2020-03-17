using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Details;

namespace MyPortal.Logic.Services
{
    public class ProfileLogNoteService : BaseService, IProfileLogNoteService
    {
        private readonly IProfileLogNoteRepository _repository;

        public ProfileLogNoteService(IProfileLogNoteRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProfileLogNoteDetails>> GetByStudent(Guid studentId)
        {
            var logNotes = await _repository.GetByStudent(studentId);

            return logNotes.Select(_businessMapper.Map<ProfileLogNoteDetails>);
        }
    }
}
