using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IAcademicYearService : IService
    {
        Task<AcademicYearModel> GetCurrent(bool getLatestIfNull = false);
        Task<AcademicYearModel> GetById(Guid academicYearId);
        Task<IEnumerable<AcademicYearModel>> GetAll();
        Task Create(AcademicYearModel academicYearModel);
        Task Update(params AcademicYearModel[] academicYearModels);
        Task Delete(params Guid[] academicYearIds);
        Task<bool> IsLocked(Guid academicYearId);
        
    }
}
