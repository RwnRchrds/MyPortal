using System.Linq;
using System.Web.Mvc;
using MyPortal.Models;
using MyPortal.ViewModels;

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
            var viewModel = new SalesViewModel();
            return View(viewModel);
        }

        [Route("Staff/Finance/Sales/New")]
        public ActionResult SaleEntry()
        {
            var viewModel = new SaleEntryViewModel()
            {
                Products = _context.Products
            };
            return View(viewModel);
        }
        
    }
}