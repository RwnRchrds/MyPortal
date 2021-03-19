using System;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Interfaces.Services
{ 
    public interface IAttendancePeriodService
    {
        Task<AttendancePeriodModel> GetById(Guid periodId);
    }
}
