using MyPortal.Database.Models;

namespace MyPortal.Logic.Models.Business
{
    public class PersonDocumentUpload
    {
        public Document Document { get; set; }
        public int PersonId { get; set; }
    }
}