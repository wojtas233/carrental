using CarRental.Business.Interfaces;
using CarRental.Business.Models;
using CarRental.DataAccess;
using CarRental.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Business.Implementations
{
    public class ReservationEquipmentBusiness : IReservationEquipmentBusiness
    {
        private readonly IApplicationDbContext _dbContext;
        public ReservationEquipmentBusiness(IApplicationDbContext db)
        {
            _dbContext = db;
        }

        public List<ReservationEquipmentModel> GetAll(int reservationId)
        {
            var model = (from reservationEquipment in _dbContext.ReservationEquipments
                          join equipment in _dbContext.Equipments
                            on reservationEquipment.Equipment.Id equals equipment.Id
                         join reservation in _dbContext.Reservations
                            on reservationEquipment.Reservation.Id equals reservation.Id
                         where reservation.Id == reservationId
                         select new ReservationEquipmentModel
                          {
                              Id = reservationEquipment.Id,
                              EquipmentId = equipment.Id,
                              ReservationId = reservationId,
                              EquipmentName = equipment.Name,
                              EquipmentPrice = equipment.Price
                          }).ToList();

            return model;
        }

        public void Add(ReservationEquipmentModel model)
        {
            var reservation = _dbContext.Reservations.SingleOrDefault(x => x.Id == model.ReservationId);
            var equipment = _dbContext.Equipments.SingleOrDefault(x => x.Id == model.EquipmentId);

            var dbModel = new ReservationEquipment
            {
                Equipment = equipment,
                Reservation = reservation,
            };
            _dbContext.ReservationEquipments.Add(dbModel);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var dbModel = _dbContext.ReservationEquipments.SingleOrDefault(x => x.Id == id);
            _dbContext.ReservationEquipments.Remove(dbModel);

            _dbContext.SaveChanges();
        }

        public ReservationEquipmentModel GetReservationEquipment(int? id)
        {
            var dbModel = (from reservationEquipment in _dbContext.ReservationEquipments
                           join equipment in _dbContext.Equipments
                             on reservationEquipment.Equipment.Id equals equipment.Id
                           join reservation in _dbContext.Reservations
                            on reservationEquipment.Reservation.Id equals reservation.Id
                           where reservationEquipment.Id == id
                           select new ReservationEquipmentModel
                           {
                               Id = reservationEquipment.Id,
                               EquipmentId = equipment.Id,
                               ReservationId = reservation.Id,
                               EquipmentName = equipment.Name,
                               EquipmentPrice = equipment.Price
                           }).SingleOrDefault();

            return dbModel;
        }
    }
}
