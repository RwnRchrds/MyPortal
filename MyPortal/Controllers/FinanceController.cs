using System.Web.Mvc;
using MyPortal.Models.Attributes;
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
        public ActionResult Products()
        {
            var viewModel = new NewProductViewModel();

            viewModel.ProductTypes = PrepareResponseObject(FinanceProcesses.GetProductTypes_Model(_context));

            return View(viewModel);
        }

        [RequiresPermission("EditSales")]
        [Route("Sales/New", Name = "FinanceSaleEntry")]
        public ActionResult SaleEntry()
        {
            var products = PrepareResponseObject(FinanceProcesses.GetAllProducts_Model(_context));

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