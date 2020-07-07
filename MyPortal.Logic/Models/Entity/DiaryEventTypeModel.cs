using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class DiaryEventTypeModel : LookupItemModel
    {
        public string ColourCode { get; set; }
        
        public bool System { get; set; }
        
        public bool Reserved { get; set; }
    }
}