using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarRental.DataAccess;
using CarRental.DataAccess.Entities;
using CarRental.Business.Interfaces;
using ExpressMapper;
using CarRental.Business.Models;
using CarRental.WebApp.Areas.UserPanel.Models;
using Microsoft.AspNet.Identity;
using System.Globalization;

namespace CarRental.WebApp.Areas.UserPanel.Controllers
{
    [Authorize(Roles = "User")]
    public class ReservationController : Controller
    {
        private readonly IReservationBusiness _reservationBusiness;
        public ReservationController(IReservationBusiness reservationBusiness)
        {
            _reservationBusiness = reservationBusiness;
        }
        
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var dbModel = _reservationBusiness.GetAll(userId);
            var model = Mapper.Map<List<ReservationDetailsModel>, List<ReservationDetailsViewModel>>(dbModel);
            return View(model);
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dbModel = _reservationBusiness.GetReservation(id);
            if (dbModel == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<ReservationDetailsModel, ReservationDetailsViewModel>(dbModel);
            return View(model);
        }
        
        public ActionResult Create(ReservationDetailsViewModel model) // int availabilityId, string locationName, DateTime? availabilityDateFrom, DateTime? availabilityDateTo, DateTime? dateFrom, DateTime? dateTo
        {
            return View("Modify", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Modify(ReservationDetailsViewModel model)
        {
            if (!(model.AvailabilityDateFrom < model.DateTo && model.DateTo <= model.AvailabilityDateTo
                && model.DateFrom < model.AvailabilityDateTo && model.DateFrom >= model.AvailabilityDateFrom))
            {
                ModelState.AddModelError("", "Reservation is not in availability range");
            }

            if (ModelState.IsValid)
            {
                model.UserId = User.Identity.GetUserId();
                var dbModel = Mapper.Map<ReservationDetailsViewModel, ReservationModel>(model);

                var isAdded = _reservationBusiness.Add(dbModel);
                if (!isAdded)
                {
                    ModelState.AddModelError("", "Reservation exists.");
                    return View("Modify", model);
                }

                return RedirectToAction("Index", "Reservation", new { area = "UserPanel" });
                //return RedirectToAction("Index", "ReservationEquipment", new { area = "UserPanel" });
            }

            return View("Modify", model);
        }
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dbModel = _reservationBusiness.GetReservation(id);
            if (dbModel == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<ReservationDetailsModel, ReservationDetailsViewModel>(dbModel);

            return View(model);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _reservationBusiness.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
