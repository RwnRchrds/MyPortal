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
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                if (await unitOfWork.AcademicYears.IsLocked(academicYearId))
                {
                    if (throwException)
                    {
                        Path.GetExtension("");
                        throw new LogicException("This academic year is locked and cannot be modified.");
                    }
                    
                    return true;
                }

                return false;
            }
        }
    }
}