using System.Threading.Tasks;
using System.Web.Mvc;
using MyPortal.Areas.Staff.ViewModels;
using MyPortal.Attributes.MvcAuthorise;
using MyPortal.BusinessLogic.Models.Identity;
using MyPortal.BusinessLogic.Services;
using MyPortal.Controllers;

namespace MyPortal.Areas.Staff.Controllers
{
    [UserType(UserType.Staff)]
    [RouteArea("Staff")]
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
            using (var profilesService = new ProfilesService())
            {
                var viewModel = new CommentsViewModel();
                viewModel.CommentBanks = await profilesService.GetAllCommentBanks();

                return View(viewModel);
            }
        }
    }
}