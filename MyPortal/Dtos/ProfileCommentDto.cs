namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// A comment that can be used in the creation of a log note.
    /// </summary>
    
    public partial class ProfileCommentDto
    {
        public int Id { get; set; }

        public int CommentBankId { get; set; }

        
        public string Value { get; set; }

        public virtual ProfileCommentBankDto CommentBank { get; set; }
    }
}
