using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class AcademicYearModel : BaseModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(128)]
        public string Name { get; set; }

        public bool Locked { get; set; }

        public static async Task CheckLock(IAcademicYearRepository academicYearRepository, Guid academicYearId)
        {
            if (await academicYearRepository.IsLocked(academicYearId))
            {
                throw new LogicException("This academic year is locked and cannot be modified.");
            }
        }
    }
}
