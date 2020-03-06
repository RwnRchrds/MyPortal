using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Dictionaries;
using MyPortalCore.Areas.Staff.ViewModels.Student;

namespace MyPortalCore.Areas.Staff.Controllers
{
    public class StudentController : BaseController
    {
        public IActionResult Index()
        {
            var viewModel = new StudentSearchViewModel();
            
            return View(viewModel);
        }
    }
}