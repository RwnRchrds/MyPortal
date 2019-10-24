using System.Threading.Tasks;
using System.Web.Mvc;
using MyPortal.Attributes;
using MyPortal.Processes;
using MyPortal.ViewModels;

namespace MyPortal.Controllers
{
    [Authorize]
    [RoutePrefix("Staff/Finance")]
    public class FinanceController : MyPortalController
    {
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        
        [RequiresPermission("EditAccounts")]
        [Route("Accounts", Name = "FinanceAccounts")]
        public ActionResult Accounts()
        {
            return View();
        }

        [RequiresPermission("EditProducts")]
        [Route("Products", Name = "FinanceProducts")]
        public async Task<ActionResult> Products()
        {
            var viewModel = new NewProductViewModel();

            viewModel.ProductTypes = await FinanceProcesses.GetAllProductTypesModel(_context);

            return View(viewModel);
        }

        [RequiresPermission("EditSales")]
        [Route("Sales/New", Name = "FinanceSaleEntry")]
        public async Task<ActionResult> SaleEntry()
        {
            var products = await FinanceProcesses.GetAllProductsModel(_context);

            var viewModel = new SaleEntryViewModel
            {
                Products = products
            };

            return View(viewModel);
        }

        [RequiresPermission("EditSales")]
        [Route("Sales", Name = "FinanceSales")]
        public ActionResult Sales()
        {
            var viewModel = new SalesViewModel();
            return View(viewModel);
        }
    }
}