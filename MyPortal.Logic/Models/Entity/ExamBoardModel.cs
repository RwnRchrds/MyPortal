using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamBoardModel : BaseModel
    {
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