using System;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IAttendanceWeekService
    {
        Task<AttendanceWeekModel> GetById(Guid attendanceWeekId);
        Task<AttendanceWeekModel> GetByDate(DateTime date, bool throwIfNotFound = true);
    }
}
