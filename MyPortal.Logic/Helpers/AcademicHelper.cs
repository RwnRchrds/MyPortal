using System;
using System.IO;
using System.Threading.Tasks;
using MyPortal.Logic.Exceptions;

namespace MyPortal.Logic.Helpers
{
    internal static class AcademicHelper
    {
        internal static async Task<bool> IsAcademicYearLocked(Guid academicYearId, bool throwException = false)
        {
            await using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                if (await unitOfWork.AcademicYears.IsLocked(academicYearId))
                {
                    if (throwException)
                    {
                        throw new LogicException("This academic year is locked and cannot be modified.");
                    }
                    
                    return true;
                }

                return false;
            }
        }
        
        internal static async Task<bool> IsAcademicYearLockedByWeek(Guid attendanceWeekId, bool throwException = false)
        {
            await using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                if (await unitOfWork.AcademicYears.IsLockedByWeek(attendanceWeekId))
                {
                    if (throwException)
                    {
                        throw new LogicException("This academic year is locked and cannot be modified.");
                    }
                    
                    return true;
                }

                return false;
            }
        }
    }
}