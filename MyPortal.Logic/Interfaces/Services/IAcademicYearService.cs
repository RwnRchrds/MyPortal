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
        Task<AcademicYearModel> CreateAcademicYear(AcademicYearRequestModel model);
        Task UpdateAcademicYear(Guid academicYearId, AcademicYearRequestModel academicYear);
        Task DeleteAcademicYear(Guid academicYearId);
        Task<bool> IsAcademicYearLocked(Guid academicYearId);
        AcademicTermRequestModel GenerateAttendanceWeeks(AcademicTermRequestModel academicYear);
    }
}
