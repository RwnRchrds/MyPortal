using System.Collections.Generic;

namespace MyPortal.Database.Models.Entity;

public class CommentType : BaseTypes.LookupItem
{
    public virtual ICollection<Comment> Comments { get; set; }
}