using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("UserRefreshTokens")]
    public class RefreshToken : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid UserId { get; set; }

        [Column(Order = 2)]
        public string Value { get; set; }

        [Column(Order = 3)]
        public DateTime ExpirationDate { get; set; }

        public virtual User User { get; set; }
    }
}
