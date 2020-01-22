using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("PhoneNumber")]
    public class PhoneNumber
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public int PersonId { get; set; }

        [Phone]
        [StringLength(128)]
        public string Number { get; set; }  

        public virtual PhoneNumberType Type { get; set; }
        public virtual Person Person { get; set; }
    }
}