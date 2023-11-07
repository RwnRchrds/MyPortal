using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.Curriculum
{
    public class CurriculumYearGroupModel : BaseModel
    {
        public CurriculumYearGroupModel(CurriculumYearGroup model) : base(model)
        {
            Name = model.Name;
            KeyStage = model.KeyStage;
            Code = model.Code;
        }

        [Required] [StringLength(128)] public string Name { get; set; }

        public int KeyStage { get; set; }

        [Required] [StringLength(10)] public string Code { get; set; }
    }
}