using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Logic.Models.Requests.Person
{
    public class PersonAttachmentUpload
    {
        public Document Document { get; set; }
        public int PersonId { get; set; }
    }
}