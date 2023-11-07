using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.Curriculum
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
    }
}