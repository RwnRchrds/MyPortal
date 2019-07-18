using System.Linq;
using System.Web.Mvc;
using MyPortal.Models;
using MyPortal.Models.Database;
using MyPortal.ViewModels;

namespace MyPortal.Controllers
{
    [Authorize(Roles = "Finance")]
    [RoutePrefix("Staff")]
    public class FinanceController : MyPortalController
    {
        [Route("Finance/Accounts")]
        public ActionResult Accounts()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [Route("Finance/Products")]
        public ActionResult Products()
        {
            var viewModel = new NewProductViewModel();

            viewModel.ProductTypes = _context.FinanceProductTypes.ToList().OrderBy(x => x.Description);

            return View(viewModel);
        }

        [Route("Finance/Sales/New")]
        public ActionResult SaleEntry()
        {
            var products = _context.FinanceProducts.OrderBy(x => x.Description).ToList();

            var viewModel = new SaleEntryViewModel
            {
                Products = products
            };

            return View(viewModel);
        }

        [Route("Finance/Sales")]
        public ActionResult Sales()
        {
            var viewModel = new SalesViewModel();
            return View(viewModel);
        }
    }
}