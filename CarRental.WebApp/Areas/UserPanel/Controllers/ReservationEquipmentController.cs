using CarRental.Business.Interfaces;
using CarRental.Business.Models;
using CarRental.WebApp.Areas.UserPanel.Models;
using ExpressMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CarRental.WebApp.Areas.UserPanel.Controllers
{
    [Authorize(Roles = "User")]
    public class ReservationEquipmentController : Controller
    {
        private readonly IReservationEquipmentBusiness _reservationEquipmentBusiness;
        private readonly IEquipmentBusiness _equipmentBusiness;
        public ReservationEquipmentController(IReservationEquipmentBusiness reservationEquipmentBusiness, IEquipmentBusiness equipmentBusiness)
        {
            _reservationEquipmentBusiness = reservationEquipmentBusiness;
            _equipmentBusiness = equipmentBusiness;
        }
        
        public ActionResult Index(int reservationId)
        {
            var dbModel = _reservationEquipmentBusiness.GetAll(reservationId);

            var equipments = Mapper.Map<List<ReservationEquipmentModel>, List<ReservationEquipmentListItemVM>>(dbModel);
            var model = new ReservationEquipmentListViewModel
            {
                ReservationId = reservationId,
                Equipments = equipments
            };

            return View(model);
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dbModel = _reservationEquipmentBusiness.GetReservationEquipment(id);
            if (dbModel == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<ReservationEquipmentModel, ReservationEquipmentListItemVM>(dbModel);
            return View(model);
        }
        
        public ActionResult Create(int reservationId)
        {
            var equipments = _equipmentBusiness.GetListItems();
            var equipmentViewModels = Mapper.Map<List<EquipmentListItemModel>, List<EquipmentViewModel>>(equipments);
            var model = new ModifyReservationEquipmentVM
            {
                ReservationId = reservationId,
                Equipments = equipmentViewModels
            };

            return View("Modify", model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Modify(ModifyReservationEquipmentVM model)
        {
            if (ModelState.IsValid)
            {
                var dbModel = Mapper.Map<ModifyReservationEquipmentVM, ReservationEquipmentModel>(model);

                _reservationEquipmentBusiness.Add(dbModel);

                return RedirectToAction("Index", "ReservationEquipment", new { area = "UserPanel", reservationId = model.ReservationId });
            }

            return View("Modify", model);
        }
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dbModel = _reservationEquipmentBusiness.GetReservationEquipment(id);
            if (dbModel == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<ReservationEquipmentModel, ReservationEquipmentListItemVM>(dbModel);

            return View(model);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int reservationId)
        {
            _reservationEquipmentBusiness.Delete(id);
            return RedirectToAction("Index", "ReservationEquipment", new { area = "UserPanel", reservationId = reservationId });
        }
    }
}