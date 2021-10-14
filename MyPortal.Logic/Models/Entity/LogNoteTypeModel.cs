using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
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