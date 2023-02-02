using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.Calendar
{
    public class DiaryEventTypeModel : LookupItemModel
    {
        public string ColourCode { get; set; }
        
        public bool System { get; set; }

        public DiaryEventTypeModel(DiaryEventType model) : base(model)
        {
            ColourCode = model.ColourCode;
            System = model.System;
        }
    }
}