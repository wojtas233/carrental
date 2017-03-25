using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRental.WebApp.Areas.UserPanel.Controllers
{
    [Authorize(Roles = "User")]
    public class UserController : Controller
    {
        // GET: UserPanel/User
        public ActionResult Index()
        {
            return View();
        }
    }
}