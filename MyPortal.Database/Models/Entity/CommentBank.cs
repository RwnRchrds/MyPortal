using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("CommentBanks")]
    public class CommentBank : BaseTypes.LookupItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CommentBank()
        {
            Areas = new HashSet<CommentBankArea>();
        }

        public virtual ICollection<CommentBankArea> Areas { get; set; }
    }
}
