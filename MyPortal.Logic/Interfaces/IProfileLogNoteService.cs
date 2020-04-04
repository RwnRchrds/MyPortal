using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Business;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Interfaces
{
    public interface IProfileLogNoteService
    {
        Task<ProfileLogNoteModel> GetById(Guid logNoteId);
        Task<IEnumerable<ProfileLogNoteModel>> GetByStudent(Guid studentId, Guid academicYearId);
        Task<Lookup> GetTypes();
        Task Create(params ProfileLogNoteModel[] logNoteObjects);
        Task Update(params ProfileLogNoteModel[] logNoteObjects);
        Task Delete(params Guid[] logNoteIds);
    }
}
