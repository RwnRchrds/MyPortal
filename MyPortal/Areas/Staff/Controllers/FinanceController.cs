using System.Threading.Tasks;
using System.Web.Mvc;
using MyPortal.Areas.Staff.ViewModels;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.Controllers;
using MyPortal.Models;
using MyPortal.Services;

namespace MyPortal.Areas.Staff.Controllers
{
    [UserType(UserType.Staff)]
    [RoutePrefix("Finance")]
    public class FinanceController : MyPortalController
    {
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
            using (var financeService = new FinanceService(UnitOfWork))
            {
                var viewModel = new NewProductViewModel {ProductTypes = await financeService.GetAllProductTypes()};

                return View(viewModel);
            }
        }

        [RequiresPermission("EditSales")]
        [Route("Sales/New", Name = "FinanceSaleEntry")]
        public async Task<ActionResult> SaleEntry()
        {
            using (var financeService =  new FinanceService(UnitOfWork))
            {
                var products = await financeService.GetAllProducts();

                var viewModel = new SaleEntryViewModel
                {
                    Products = products
                };

                return View(viewModel);
            }
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