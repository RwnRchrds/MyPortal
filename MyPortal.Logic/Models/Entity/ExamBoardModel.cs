using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamBoardModel : BaseModel
    {
        public ExamBoardModel(ExamBoard model) : base(model)
        {
            Abbreviation = model.Abbreviation;
            FullName = model.FullName;
            Code = model.Code;
            Domestic = model.Domestic;
            UseEdi = model.UseEdi;
            Active = model.Active;
        }
        
        [StringLength(20)]
        public string Abbreviation { get; set; }
        
        [StringLength(128)]
        public string FullName { get; set; }
        
        [StringLength(5)]
        public string Code { get; set; }
        
        public bool Domestic { get; set; }
        
        public bool UseEdi { get; set; }
        
        public bool Active { get; set; }
    }
}