using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortal.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        [Route("Staff/Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("Staff/Admin/Users")]
        public ActionResult Users()
        {
            return View();
        }
    }
}