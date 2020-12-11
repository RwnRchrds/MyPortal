using System;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Interfaces.Services
{ 
    public interface IPeriodService : IService
    {
        Task<AttendancePeriodModel> GetById(Guid periodId);
    }
}
