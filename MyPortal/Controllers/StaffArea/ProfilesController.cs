using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.ViewModels;

namespace MyPortal.Controllers.StaffPortal
{
    [RoutePrefix("Staff/Profiles")]
    public class ProfilesController : MyPortalController
    {
        [RequiresPermission("EditComments")]
        [Route("Profile/CommentBanks")]
        public ActionResult CommentBanks()
        {
            return View("~/Views/Staff/Profile/CommentBanks.cshtml");
        }

        [RequiresPermission("EditComments")]
        [Route("Profile/Comments")]
        public ActionResult Comments()
        {
            var viewModel = new CommentsViewModel();
            viewModel.CommentBanks = _context.ProfileCommentBanks.OrderBy(x => x.Name).ToList();

            return View("~/Views/Staff/Profile/Comments.cshtml", viewModel);
        }
    }
}