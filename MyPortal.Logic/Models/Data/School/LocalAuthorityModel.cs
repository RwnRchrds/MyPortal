using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.School
{
    public class LocalAuthorityModel : BaseModel
    {
        public LocalAuthorityModel(LocalAuthority model) : base(model)
        {
            LeaCode = model.LeaCode;
            Name = model.Name;
            Website = model.Website;
        }
        
        public int LeaCode { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public string Website { get; set; }
    }
}
