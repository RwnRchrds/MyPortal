using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface ILogNoteService : IService
    {
        Task<LogNoteModel> GetById(Guid logNoteId);
        Task<IEnumerable<LogNoteModel>> GetByStudent(Guid studentId, Guid academicYearId);
        Task<IEnumerable<LogNoteTypeModel>> GetTypes();
        Task Create(params LogNoteModel[] logNoteObjects);
        Task Update(params LogNoteModel[] logNoteObjects);
        Task Delete(params Guid[] logNoteIds);
    }
}
