using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Curriculum;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IAcademicYearService
    {
        Task<AcademicYearModel> GetCurrentAcademicYear(bool getLatestIfNull = false);
        Task<AcademicYearModel> GetAcademicYearById(Guid academicYearId);
        Task<IEnumerable<AcademicYearModel>> GetAcademicYears();
        Task CreateAcademicYear(params CreateAcademicYearRequestModel[] createModels);
        Task UpdateAcademicYear(params UpdateAcademicYearRequestModel[] academicYearModels);
        Task DeleteAcademicYear(params Guid[] academicYearIds);
        Task<bool> IsAcademicYearLocked(Guid academicYearId);
        CreateAcademicTermRequestModel[] GenerateAttendanceWeeks(params CreateAcademicTermRequestModel[] termModel);


    }
}
