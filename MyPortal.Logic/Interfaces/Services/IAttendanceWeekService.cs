using System;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IAttendanceWeekService : IService
    {
        Task<AttendanceWeekModel> GetById(Guid attendanceWeekId);
    }
}
