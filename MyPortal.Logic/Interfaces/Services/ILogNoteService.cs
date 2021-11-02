using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Student.LogNotes;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface ILogNoteService
    {
        Task<LogNoteModel> GetById(Guid logNoteId);
        Task<IEnumerable<LogNoteModel>> GetByStudent(Guid studentId, Guid academicYearId);
        Task<IEnumerable<LogNoteTypeModel>> GetTypes();
        Task Create(params CreateLogNoteModel[] logNoteObjects);
        Task Update(params UpdateLogNoteModel[] logNoteObjects);
        Task Delete(params Guid[] logNoteIds);
    }
}
