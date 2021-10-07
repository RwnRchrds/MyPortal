using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class AcademicYearModel : BaseModel
    {
        internal AcademicYearModel(AcademicYear model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(AcademicYear model)
        {
            Name = model.Name;
            Locked = model.Locked;
        }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(128)]
        public string Name { get; set; }

        public bool Locked { get; set; }

        public static async Task CheckLock(IUnitOfWork unitOfWork, Guid academicYearId)
        {
            if (await unitOfWork.AcademicYears.IsLocked(academicYearId))
            {
                throw new LogicException("This academic year is locked and cannot be modified.");
            }
        }
    }
}
