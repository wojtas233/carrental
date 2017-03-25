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
using CarRental.WebApp.Areas.AdminPanel.Models;

namespace CarRental.WebApp.Areas.AdminPanel.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LocationController : Controller
    {
        private readonly ILocationBusiness _locationBusiness;
        public LocationController(ILocationBusiness locationBusiness)
        {
            _locationBusiness = locationBusiness;
        }

        // GET: AdminPanel/City
        public ActionResult Index()
        {
            var dbModel = _locationBusiness.GetAll();
            var model = Mapper.Map<List<LocationModel>, List<LocationViewModel>>(dbModel);
            return View(model);
        }

        // GET: AdminPanel/City/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dbModel = _locationBusiness.GetLocation(id);
            if (dbModel == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<LocationModel, LocationViewModel>(dbModel);
            return View(model);
        }

        // GET: AdminPanel/City/Create
        public ActionResult Create()
        {
            var model = new LocationViewModel();
            return View("Modify", model);
        }

        // POST: AdminPanel/City/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Modify(LocationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dbModel = Mapper.Map<LocationViewModel, LocationModel>(model);
                
                if(model.Id > 0)
                {
                    _locationBusiness.Edit(dbModel);
                }
                else
                {
                    _locationBusiness.Add(dbModel);
                }

                return RedirectToAction("Index");
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
            var dbModel = _locationBusiness.GetLocation(id);
            if (dbModel == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<LocationModel, LocationViewModel>(dbModel);
            return View("Modify", model);
        }

        // GET: AdminPanel/City/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dbModel = _locationBusiness.GetLocation(id);
            if (dbModel == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<LocationModel, LocationViewModel>(dbModel);

            return View(model);
        }

        // POST: AdminPanel/City/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _locationBusiness.Delete(id);
            return RedirectToAction("Index");
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
