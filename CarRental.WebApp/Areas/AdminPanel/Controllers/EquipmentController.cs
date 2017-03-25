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
    public class EquipmentController : Controller
    {
        private readonly IEquipmentBusiness _equipmentBusiness;
        public EquipmentController(IEquipmentBusiness equipmentBusiness)
        {
            _equipmentBusiness = equipmentBusiness;
        }

        // GET: AdminPanel/City
        public ActionResult Index()
        {
            var dbModel = _equipmentBusiness.GetAll();
            var model = Mapper.Map<List<EquipmentModel>, List<EquipmentViewModel>>(dbModel);
            return View(model);
        }

        // GET: AdminPanel/City/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dbModel = _equipmentBusiness.GetEquipment(id);
            if (dbModel == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<EquipmentModel, EquipmentViewModel>(dbModel);
            return View(model);
        }

        // GET: AdminPanel/City/Create
        public ActionResult Create()
        {
            var model = new EquipmentViewModel();
            return View("Modify", model);
        }

        // POST: AdminPanel/City/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Modify(EquipmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dbModel = Mapper.Map<EquipmentViewModel, EquipmentModel>(model);

                if (model.Id > 0)
                {
                    _equipmentBusiness.Edit(dbModel);
                }
                else
                {
                    _equipmentBusiness.Add(dbModel);
                }

                return RedirectToAction("Index", "Equipment", new { area = "AdminPanel" });
            }

            return View("Modify", model);
        }

        // GET: AdminPanel/City/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dbModel = _equipmentBusiness.GetEquipment(id);
            if (dbModel == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<EquipmentModel, EquipmentViewModel>(dbModel);
            return View("Modify", model);
        }

        // GET: AdminPanel/City/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dbModel = _equipmentBusiness.GetEquipment(id);
            if (dbModel == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<EquipmentModel, EquipmentViewModel>(dbModel);

            return View(model);
        }

        // POST: AdminPanel/City/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _equipmentBusiness.Delete(id);
            return RedirectToAction("Index");
        }
    }
}