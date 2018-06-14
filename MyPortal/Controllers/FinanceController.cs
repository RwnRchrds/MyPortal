using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyPortal.Models;

namespace MyPortal.Controllers
{
    [Authorize(Roles = "Finance")]
    public class FinanceController : Controller
    {
        private MyPortalDbContext _context;

        public FinanceController()
        {
            _context = new MyPortalDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [Route("Staff/Finance")]
        public ActionResult Index()
        {
            return View();
        }
    }
}