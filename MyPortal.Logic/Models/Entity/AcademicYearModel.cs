using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;

namespace MyPortal.Logic.Models.Entity
{
    public class AcademicYearModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(128)]
        public string Name { get; set; }

        [Required(ErrorMessage = "First Date is required.")]
        public DateTime FirstDate { get; set; }

        [Required(ErrorMessage = "Last Date is required.")]
        public DateTime LastDate { get; set; }

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
