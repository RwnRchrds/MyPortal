using System.Collections.Generic;
using MyPortal.Models;

namespace MyPortal.ViewModels
{
    public class CommentsViewModel
    {
        public IEnumerable<CommentBank> CommentBanks { get; set; }
        public Comment Comment { get; set; }
    }
}