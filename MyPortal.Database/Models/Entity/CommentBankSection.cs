using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity;

[Table("CommentBankSections")]
public class CommentBankSection : BaseTypes.Entity
{
    [Column(Order = 1)]
    public Guid CommentBankAreaId { get; set; }

    [Required]
    [StringLength(256)]
    [Column(Order = 2)]
    public string Name { get; set; }
    
    public virtual CommentBankArea Area { get; set; }
    
    public virtual ICollection<Comment> Comments { get; set; }
}