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
    public class ReservationBusiness : IReservationBusiness
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly string separ = ", ";
        private readonly string space = " ";
        public ReservationBusiness(IApplicationDbContext db)
        {
            _dbContext = db;
        }

        public List<ReservationDetailsModel> GetAll(string userId)
        {
            var model = (from reserv in _dbContext.Reservations
                         join availability in _dbContext.Availabilities
                            on reserv.Availability.Id equals availability.Id
                         join reservEquipment in _dbContext.ReservationEquipments 
                            on reserv.Id equals reservEquipment.Reservation.Id into joined
                            from rejoined in joined.DefaultIfEmpty()
                         join location in _dbContext.Locations
                            on availability.Location.Id equals location.Id into avjoined
                            from ajoined in avjoined.DefaultIfEmpty()
                         where reserv.User.Id == userId
                         select new ReservationDetailsModel
                        {
                            Id = reserv.Id,
                            DateFrom = reserv.DateFrom,
                            DateTo = reserv.DateTo,
                            AvailabilityId = availability.Id,
                            LocationName = ajoined.CountryName + separ
                                            + ajoined.CityName + separ
                                            + ajoined.PostalCode + separ
                                            + ajoined.StreetName + space
                                            + ajoined.BuildingNumber + "/"
                                            + ajoined.OfficeNumber,
                         })
                         .GroupBy(x => x.Id)
                        .Select(group => group.FirstOrDefault())
                         .ToList();
            
            return model;
        }

        public bool Add(ReservationModel model)
        {
            var periodsCoincide = _dbContext.Reservations.Any(x => x.Availability.Id == model.AvailabilityId && (x.DateFrom < model.DateTo && model.DateFrom < x.DateTo));
            if (!periodsCoincide)
            {
                var availability = _dbContext.Availabilities.SingleOrDefault(x => x.Id == model.AvailabilityId);
                var user = _dbContext.Users.SingleOrDefault(x => x.Id == model.UserId);
                var dbModel = new Reservation
                {
                    Id = model.Id,
                    DateFrom = model.DateFrom,
                    DateTo = model.DateTo,
                    Availability = availability,
                    User = user
                };
                _dbContext.Reservations.Add(dbModel);
                _dbContext.SaveChanges();

                return true; // means reservation is added
            }
            return false; // means reservation is not added
        }

        public void Edit(ReservationModel model)
        {
            var dbModel = _dbContext.Reservations.SingleOrDefault(x => x.Id == model.Id);

            if (dbModel != null)
            {
                if (dbModel.DateFrom != model.DateFrom)
                {
                    dbModel.DateFrom = model.DateFrom;
                }
                if (dbModel.DateTo != model.DateTo)
                {
                    dbModel.DateTo = model.DateTo;
                }
               
                _dbContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var dbModel = _dbContext.Reservations.SingleOrDefault(x => x.Id == id);

            _dbContext.Reservations.Remove(dbModel);
            _dbContext.SaveChanges();
        }

        public ReservationDetailsModel GetReservation(int? id)
        {
            var dbModel = (from reserv in _dbContext.Reservations
                           join availability in _dbContext.Availabilities
                              on reserv.Availability.Id equals availability.Id
                           join reservEquipment in _dbContext.ReservationEquipments
                              on reserv.Id equals reservEquipment.Reservation.Id into joined
                           from rejoined in joined.DefaultIfEmpty()
                           join location in _dbContext.Locations
                              on availability.Location.Id equals location.Id into avjoined
                           from ajoined in avjoined.DefaultIfEmpty()
                           where reserv.Id == id
                           select new ReservationDetailsModel
                           {
                               Id = reserv.Id,
                               DateFrom = reserv.DateFrom,
                               DateTo = reserv.DateTo,
                               AvailabilityId = availability.Id,
                               LocationName = ajoined.CountryName + separ
                                              + ajoined.CityName + separ
                                              + ajoined.PostalCode + separ
                                              + ajoined.StreetName + space
                                              + ajoined.BuildingNumber + "/"
                                              + ajoined.OfficeNumber,
                           }).SingleOrDefault();

            return dbModel;
        }
    }
}
