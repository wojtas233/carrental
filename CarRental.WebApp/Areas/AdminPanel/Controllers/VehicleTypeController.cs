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
using CarRental.WebApp.Areas.AdminPanel.Models;
using CarRental.Business.Models;

namespace CarRental.WebApp.Areas.AdminPanel.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VehicleTypeController : Controller
    {
        private readonly IVehicleTypeBusiness _vehicleTypeBusiness;

        public VehicleTypeController(IVehicleTypeBusiness vehicleTypeBusiness)
        {
            _vehicleTypeBusiness = vehicleTypeBusiness;
        }

        // GET: AdminPanel/VehicleType
        public ActionResult Index()
        {
            var dbModel = _vehicleTypeBusiness.GetAll();
            var model = Mapper.Map<List<VehicleTypeModel>, List<VehicleTypeViewModel>>(dbModel);
            return View(model);
        }

        // GET: AdminPanel/VehicleType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dbModel = _vehicleTypeBusiness.GetVehicleType(id);
            if (dbModel == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<VehicleTypeModel, VehicleTypeViewModel>(dbModel);
            return View(model);
        }

        // GET: AdminPanel/VehicleType/Create
        public ActionResult Create()
        {
            var model = new VehicleTypeViewModel();
            var image = new ImageViewModel();
            model.Image = image;
            return View("Modify", model);
        }

        // POST: AdminPanel/VehicleType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Modify(VehicleTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dbModel = Mapper.Map<VehicleTypeViewModel, VehicleTypeModel>(model);
                if (Request.Files != null && Request.Files.Count > 0)
                {
                    dbModel.ImageFile = Request.Files[0];
                }
                if (model.Id > 0) // Edit
                {
                    _vehicleTypeBusiness.Edit(dbModel);
                    return RedirectToAction("Index");
                }
                else // Add
                {
                    _vehicleTypeBusiness.Add(dbModel);
                    return RedirectToAction("Index", "VehicleType", new { area = "AdminPanel" });
                }
            }

            return View("Modify", model);
        }

        // GET: AdminPanel/VehicleType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dbModel = _vehicleTypeBusiness.GetVehicleType(id);
            if (dbModel == null)
            {
                return HttpNotFound();
            }
            var model = Mapper.Map<VehicleTypeModel, VehicleTypeViewModel>(dbModel);

            return View("Modify", model);
        }
        
        // GET: AdminPanel/VehicleType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dbModel = _vehicleTypeBusiness.GetVehicleType(id);
            if (dbModel == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<VehicleTypeModel, VehicleTypeViewModel>(dbModel);

            return View(model);
        }

        // POST: AdminPanel/VehicleType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _vehicleTypeBusiness.Delete(id);
            return RedirectToAction("Index", "VehicleType", new { area = "AdminPanel" });
        }

        protected override void Dispose(bool disposing)
        {
            //if (disposing)
            //{
            //    db.Dispose();
            //}
            //base.Dispose(disposing);
        }
    }
}
