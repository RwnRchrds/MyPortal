using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.Assessment
{
    public class MarksheetTemplateModel : BaseModel
    {
        public MarksheetTemplateModel(MarksheetTemplate model) : base(model)
        {
            Name = model.Name;
            Active = model.Active;
            Notes = model.Notes;
        }

        public string Notes { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }
    }
}
