using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
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