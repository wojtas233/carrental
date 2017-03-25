using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarRental.DataAccess;
using CarRental.WebApp.Areas.AdminPanel.Models;
using CarRental.Business.Interfaces;
using ExpressMapper;
using CarRental.Business.Models;

namespace CarRental.WebApp.Areas.AdminPanel.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VehicleImageController : Controller
    {
        private readonly IVehicleImageBusiness _vehicleImageBusiness;
        public VehicleImageController(IVehicleImageBusiness vehicleImageBusiness)
        {
            _vehicleImageBusiness = vehicleImageBusiness;
        }

        // GET: AdminPanel/City
        public ActionResult Index(int vehicleId, bool isEditMode = false)
        {
            var dbModel = _vehicleImageBusiness.GetAll(vehicleId);
            var model = Mapper.Map<List<VehicleImageModel>, List<VehicleImageViewModel>>(dbModel);

            var viewModel = new VehicleImagesViewModel
            {
                VehicleId = vehicleId,
                Images = model,
                IsEditMode = isEditMode
            };

            return View(viewModel);
        }

        // GET: AdminPanel/City/Create
        public ActionResult Create(int vehicleId, bool isEditMode = false)
        {
            var model = new VehicleImageViewModel {
                VehicleId = vehicleId,
                IsEditMode = isEditMode
            };
            return View("Modify", model);
        }

        // POST: AdminPanel/City/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Modify(VehicleImageViewModel model, bool isEditMode = false)
        {
            if (ModelState.IsValid)
            {
                var dbModel = Mapper.Map<VehicleImageViewModel, VehicleImageModel>(model);
                if (Request.Files != null && Request.Files.Count > 0)
                {
                    dbModel.ImageFile = Request.Files[0];
                }
                if (model.Id > 0)
                {
                    _vehicleImageBusiness.Edit(dbModel);
                }
                else
                {
                    _vehicleImageBusiness.Add(dbModel);
                }

                return RedirectToAction("Index", new { vehicleId = model.VehicleId, isEditMode = isEditMode });
            }

            return View("Modify", model);
        }

        // GET: AdminPanel/City/Edit/5
        public ActionResult Edit(int? id, bool isEditMode = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dbModel = _vehicleImageBusiness.GetImage(id);
            if (dbModel == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<VehicleImageModel, VehicleImageViewModel>(dbModel);
            model.IsEditMode = isEditMode;

            return View("Modify", model);
        }

        // GET: AdminPanel/City/Delete/5
        public ActionResult Delete(int? id, bool isEditMode = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dbModel = _vehicleImageBusiness.GetImage(id);
            if (dbModel == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<VehicleImageModel, VehicleImageViewModel>(dbModel);
            model.IsEditMode = isEditMode;
            return View(model);
        }

        // POST: AdminPanel/City/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int vehicleId, bool isEditMode = false)
        {
            _vehicleImageBusiness.Delete(id);
            return RedirectToAction("Index", new { vehicleId = vehicleId, isEditMode = isEditMode });
        }
    }
}
