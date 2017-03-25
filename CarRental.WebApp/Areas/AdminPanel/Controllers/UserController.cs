using CarRental.Business.Interfaces;
using CarRental.Business.Models;
using CarRental.WebApp.Areas.AdminPanel.Models;
using ExpressMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRental.WebApp.Areas.AdminPanel.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserBusiness _userBusiness;

        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        public ActionResult Index()
        {
            var dbModel = _userBusiness.GetAll();
            var model = Mapper.Map<List<UserModel>, List<UserViewModel>>(dbModel);
            return View(model);
        }
    }
}