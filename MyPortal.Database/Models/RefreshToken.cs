using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("RefreshTokens")]
    public class RefreshToken : Entity
    {
        [Column(Order = 1)]
        public Guid UserId { get; set; }

        [Column(Order = 2)]
        public string Token { get; set; }

        [Column(Order = 3)]
        public DateTime ExpirationDate { get; set; }

        public virtual User User { get; set; }
    }
}
