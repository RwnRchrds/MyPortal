using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Database.Models.Entity;

public class CommentBankSection : BaseTypes.Entity
{
    public Guid CommentBankAreaId { get; set; }

    [Required]
    [StringLength(256)]
    public string Name { get; set; }
    
    public virtual CommentBankArea Area { get; set; }
    
    public virtual ICollection<Comment> Comments { get; set; }
}