using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("PersonAttachment")]
    public class PersonAttachment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid PersonId { get; set; }

        public Guid DocumentId { get; set; }

        public virtual Document Document { get; set; }

        public virtual Person Person { get; set; }
    }
}