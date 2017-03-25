using CarRental.Business.Interfaces;
using CarRental.Business.Models;
using CarRental.WebApp.Areas.AdminPanel.Models;
using ExpressMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CarRental.WebApp.Areas.AdminPanel.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VehicleController : Controller
    {
        private readonly IVehicleBusiness _vehicleBusiness;
        private readonly IVehicleTypeBusiness _vehicleTypeBusiness;

        public VehicleController(IVehicleBusiness vehicleBusiness, IVehicleTypeBusiness vehicleTypeBusiness)
        {
            _vehicleBusiness = vehicleBusiness;
            _vehicleTypeBusiness = vehicleTypeBusiness;
        }
        
        public ActionResult Index()
        {
            var dbModel = _vehicleBusiness.GetAll();
            var model = Mapper.Map<List<VehicleModel>, List<VehicleViewModel>>(dbModel);
            return View("VehiclesList", model);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var dbModel = _vehicleBusiness.GetVehicle(id);
            if (dbModel == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<VehicleModel, VehicleViewModel>(dbModel);
            return View(model);
        }

        public ActionResult Create()
        {
            var model = new VehicleViewModel();
            model.VehicleTypes = Mapper.Map<List<VehicleTypeModel>, List<VehicleTypeViewModel>>(_vehicleTypeBusiness.GetAll());
            return View("Modify", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Modify(VehicleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dbModel = Mapper.Map<VehicleViewModel, VehicleModel>(model);
                if (model.Id > 0) // Edit
                {
                    _vehicleBusiness.Edit(dbModel);

                    return RedirectToAction("Edit", "Vehicle", new { area = "AdminPanel", id = dbModel.Id, isSuccessModified = true });
                }
                else // Add
                {
                    var vehicleId = _vehicleBusiness.Add(dbModel);
                    return RedirectToAction("Index", "VehicleImage", new { area = "AdminPanel", vehicleId = vehicleId });
                }
            }

            return View("Modify", model);
        }

        public ActionResult Edit(int? id, bool isSuccessModified = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dbModel = _vehicleBusiness.GetVehicle(id);
            if (dbModel == null)
            {
                return HttpNotFound();
            }
            var model = Mapper.Map<VehicleModel, VehicleViewModel>(dbModel);
            model.VehicleTypes = Mapper.Map<List<VehicleTypeModel>, List<VehicleTypeViewModel>>(_vehicleTypeBusiness.GetAll());
            model.IsSuccessModified = isSuccessModified;

            return View("Modify", model);
        }

        // GET: Quizs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dbModel = _vehicleBusiness.GetVehicle(id);
            if (dbModel == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<VehicleModel, VehicleViewModel>(dbModel);

            return View(model);
        }

        // POST: Quizs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _vehicleBusiness.Delete(id);
            return RedirectToAction("Index");
        }
    }
}