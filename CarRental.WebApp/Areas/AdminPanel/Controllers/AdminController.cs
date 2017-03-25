using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRental.WebApp.Areas.AdminPanel.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: AdminPanel/AdminPanel
        public ActionResult Index()
        {
            return View("Index", new { area = "AdminPanel" });
        }
    }
}