using System.Threading.Tasks;
using System.Web.Mvc;
using MyPortal.Areas.Staff.ViewModels;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.Controllers;
using MyPortal.Services;

namespace MyPortal.Areas.Staff.Controllers
{
    [RoutePrefix("Profiles")]
    public class ProfilesController : MyPortalController
    {
        [RequiresPermission("EditComments")]
        [Route("CommentBanks")]
        public ActionResult CommentBanks()
        {
            return View();
        }

        [RequiresPermission("EditComments")]
        [Route("Profile/Comments")]
        public async Task<ActionResult> Comments()
        {
            using (var profilesService = new ProfilesService(UnitOfWork))
            {
                var viewModel = new CommentsViewModel();
                viewModel.CommentBanks = await profilesService.GetAllCommentBanks();

                return View(viewModel);
            }
        }
    }
}