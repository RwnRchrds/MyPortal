using System;
using System.Threading.Tasks;
using MyPortal.Logic.Exceptions;

namespace MyPortal.Logic.Helpers
{
    public static class AcademicHelper
    {
        public static async Task<bool> IsAcademicYearLocked(Guid academicYearId, bool throwException = false)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
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
    }
}