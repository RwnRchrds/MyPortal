using System.Collections.Generic;
using MyPortal.Models.Database;

namespace MyPortal.Areas.Staff.ViewModels
{
    public class CommentsViewModel
    {
        public IEnumerable<ProfileCommentBank> CommentBanks { get; set; }
        public ProfileComment Comment { get; set; }
    }
}