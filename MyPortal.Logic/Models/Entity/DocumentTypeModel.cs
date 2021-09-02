using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class DocumentTypeModel : LookupItemModel
    {
        public DocumentTypeModel(DocumentType model) : base(model)
        {
            Staff = model.Staff;
            Student = model.Student;
            Contact = model.Contact;
            General = model.General;
            Sen = model.Sen;
        }
        
        public bool Staff { get; set; }
        public bool Student { get; set; }
        public bool Contact { get; set; }
        public bool General { get; set; }
        public bool Sen { get; set; }
    }
}
