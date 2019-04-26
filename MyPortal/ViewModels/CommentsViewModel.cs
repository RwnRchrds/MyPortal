using System.Collections.Generic;
using MyPortal.Models;
using MyPortal.Models.Database;

namespace MyPortal.ViewModels
{
    public class CommentsViewModel
    {
        public IEnumerable<ProfileCommentBank> CommentBanks { get; set; }
        public ProfileComment Comment { get; set; }
    }
}