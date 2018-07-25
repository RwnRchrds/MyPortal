using System.Web.Mvc;
using MyPortal.Models;

namespace MyPortal.Controllers
{
    [Authorize(Roles = "Finance")]
    public class FinanceController : Controller
    {
        private readonly MyPortalDbContext _context;

        public FinanceController()
        {
            _context = new MyPortalDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [Route("Staff/Finance/Products")]
        public ActionResult Products()
        {
            return View();
        }

        [Route("Staff/Finance/Accounts")]
        public ActionResult Accounts()
        {
            return View();
        }

        [Route("Staff/Finance/Sales")]
        public ActionResult Sales()
        {
            return View();
        }

        //TODO: Dinner Money
    }
}