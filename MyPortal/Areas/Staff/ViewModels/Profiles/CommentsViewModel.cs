using System.Collections.Generic;
using MyPortal.BusinessLogic.Dtos;

namespace MyPortal.Areas.Staff.ViewModels.Profiles
{
    public class CommentsViewModel
    {
        public IEnumerable<CommentBankDto> CommentBanks { get; set; }
        public CommentDto Comment { get; set; }
    }
}