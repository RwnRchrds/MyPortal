using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class BoarderStatusModel : LookupItemModel
    {
        public BoarderStatusModel(BoarderStatus model) : base(model)
        {
            Code = model.Code;
        }
        
        public string Code { get; set; }
    }
}