using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("UserRefreshTokens")]
    public class RefreshToken : Entity
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
