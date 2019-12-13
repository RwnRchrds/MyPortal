using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Data.Models
{
    /// <summary>
    /// A document assigned to a person.
    /// </summary>
    [Table("PersonAttachment", Schema = "document")]
    public class PersonAttachment
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public int DocumentId { get; set; }

        public virtual Document Document { get; set; }

        public virtual Person Person { get; set; }
    }
}