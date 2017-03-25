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
using CarRental.WebApp.Areas.AdminPanel.Models;
using ExpressMapper;
using CarRental.Business.Models;

namespace CarRental.WebApp.Areas.AdminPanel.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AvailabilityController : Controller
    {
        private readonly IAvailabilityBusiness _availabilityBusiness;
        private readonly ILocationBusiness _locationBusiness;
        private readonly IVehicleBusiness _vehicleBusiness;

        public AvailabilityController(IAvailabilityBusiness availabilityBusiness, ILocationBusiness locationBusiness, IVehicleBusiness vehicleBusiness)
        {
            _availabilityBusiness = availabilityBusiness;
            _locationBusiness = locationBusiness;
            _vehicleBusiness = vehicleBusiness;
        }

        // GET: AdminPanel/Availability
        public ActionResult Index(int? vehicleId = null, bool isEditMode = false)
        {
            var dbModel = _availabilityBusiness.GetAll(vehicleId);
            var model = Mapper.Map<List<AvailabilityModel>, List<AvailabilityDetailsViewModel>>(dbModel);

            var viewModel = new AvailabilitiesViewModel
            {
                VehicleId = vehicleId.Value,
                Availabilities = model,
                IsEditMode = isEditMode
            };

            return View(viewModel);
        }

        // GET: AdminPanel/Availability/Details/5
        public ActionResult Details(int? id, bool isEditMode = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dbModel = _availabilityBusiness.GetAvailability(id);
            if (dbModel == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<AvailabilityModel, AvailabilityDetailsViewModel>(dbModel);
            model.IsEditMode = isEditMode;

            return View(model);
        }

        public AvailabilityViewModel PrepareModifyModel(int vehicleId, bool isEditMode = false)
        {
            var model = new AvailabilityViewModel
            {
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now.AddDays(1)
            };
            model.VehicleId = vehicleId;
            var locations = _locationBusiness.GetAll();
            model.Locations = Mapper.Map<List<LocationModel>, List<LocationViewModel>>(locations);
            model.IsEditMode = isEditMode;

            return model;
        }

        // GET: AdminPanel/Availability/Create
        public ActionResult Create(int vehicleId, bool isEditMode = false)
        {
            var model = PrepareModifyModel(vehicleId, isEditMode);
            model.DateFrom = DateTime.Now;
            model.DateTo = DateTime.Now.AddDays(1);

            return View("Modify", model);
        }

        // POST: AdminPanel/Availability/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Modify(AvailabilityViewModel model, bool isEditMode = false)
        {
            if (model.DateFrom.Date < DateTime.Now.Date)
            {
                ModelState.AddModelError("", "Date from must be greater than date now or equals.");
            }
            if (model.DateFrom.Date >= model.DateTo.Date)
            {
                ModelState.AddModelError("", "Date to must be greater than date from.");
            }

            if (ModelState.IsValid)
            {
                var dbModel = Mapper.Map<AvailabilityViewModel, AvailabilityModel>(model);
                if(model.Id > 0)
                {
                    _availabilityBusiness.Edit(dbModel);
                }
                else
                {
                    var isAdded = _availabilityBusiness.Add(dbModel);
                    if (!isAdded)
                    {
                        ModelState.AddModelError("", "Availability exists.");

                        var validModel = PrepareModifyModel(model.VehicleId, isEditMode);
                        model.DateFrom = model.DateFrom;
                        model.DateTo = model.DateTo;

                        return View("Modify", validModel);
                    }
                }

                return RedirectToAction("Index", new { vehicleId = model.VehicleId, isEditMode = isEditMode });
            }
            
            var viewModel = PrepareModifyModel(model.VehicleId, isEditMode);
            model.DateFrom = model.DateFrom;
            model.DateTo = model.DateTo;

            return View("Modify", viewModel);
        }

        // GET: AdminPanel/Availability/Edit/5
        public ActionResult Edit(int? id, bool isEditMode = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dbModel = _availabilityBusiness.GetAvailability(id);
            if (dbModel == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<AvailabilityModel, AvailabilityViewModel>(dbModel);

            var locations = _locationBusiness.GetAll();
            model.Locations = Mapper.Map<List<LocationModel>, List<LocationViewModel>>(locations);

            model.IsEditMode = isEditMode;

            return View("Modify", model);
        }

        // GET: AdminPanel/Availability/Delete/5
        public ActionResult Delete(int? id, bool isEditMode = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dbModel = _availabilityBusiness.GetAvailability(id);
            if (dbModel == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<AvailabilityModel, AvailabilityDetailsViewModel>(dbModel);
            model.IsEditMode = isEditMode;

            return View(model);
        }

        // POST: AdminPanel/Availability/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int vehicleId, bool isEditMode = false)
        {
            _availabilityBusiness.Delete(id);
            return RedirectToAction("Index", new { vehicleId = vehicleId, isEditMode = isEditMode });
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
