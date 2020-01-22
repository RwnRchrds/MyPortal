using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("PersonAttachment")]
    public class PersonAttachment
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public int DocumentId { get; set; }

        public virtual Document Document { get; set; }

        public virtual Person Person { get; set; }
    }
}