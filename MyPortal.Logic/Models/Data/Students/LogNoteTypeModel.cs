using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.Students
{
    public class LogNoteTypeModel : LookupItemModel
    {
        public LogNoteTypeModel(LogNoteType model) : base(model)
        {
            ColourCode = model.ColourCode;
            IconClass = model.IconClass;
        }
        
        public string ColourCode { get; set; }
        public string IconClass { get; set; }
    }
}