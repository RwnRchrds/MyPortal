namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// A comment that can be used in the creation of a log note.
    /// </summary>
    [Table("Profile_Comments")]
    public partial class ProfileComment
    {
        public int Id { get; set; }

        public int CommentBankId { get; set; }

        [Required]
        public string Value { get; set; }

        public virtual ProfileCommentBank CommentBank { get; set; }
    }
}
