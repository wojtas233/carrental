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
    public class AvailabilityBusiness : IAvailabilityBusiness
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly string separ = ", ";
        private readonly string space = " ";

        public AvailabilityBusiness(IApplicationDbContext db)
        {
            _dbContext = db;
        }

        public List<AvailabilityModel> GetAll(int? vehicleId)
        {
            var model = (from availability in _dbContext.Availabilities
                         join location in _dbContext.Locations
                            on availability.Location.Id equals location.Id
                         join vehicle in _dbContext.Vehicles
                            on availability.Vehicle.Id equals vehicle.Id
                         where vehicle.Id == vehicleId
                         select new AvailabilityModel
                         {
                            Id = availability.Id,
                            DateFrom = availability.DateFrom,
                            DateTo = availability.DateTo,
                            FullLocationName = location.CountryName + separ
                                + location.CityName + separ
                                + location.PostalCode + separ
                                + location.StreetName + space
                                + location.BuildingNumber + "/"
                                + location.OfficeNumber,
                            FullVehicleName = vehicle.Brand + space
                                + vehicle.ModelName,
                             LocationId = location.Id,
                             VehicleId = vehicle.Id
                         }).ToList();

            return model;
        }

        public bool Add(AvailabilityModel model)
        {
            var periodsCoincide = _dbContext.Availabilities.Any(x => x.Vehicle.Id == model.VehicleId && (x.DateFrom < model.DateTo && model.DateFrom < x.DateTo));
            if (!periodsCoincide)
            {
                var location = _dbContext.Locations.SingleOrDefault(x => x.Id == model.LocationId);
                var vehicle = _dbContext.Vehicles.SingleOrDefault(x => x.Id == model.VehicleId);
                var dbModel = new Availability
                {
                    Id = model.Id,
                    DateFrom = model.DateFrom,
                    DateTo = model.DateTo,
                    Location = location,
                    Vehicle = vehicle
                };
                _dbContext.Availabilities.Add(dbModel);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public void Edit(AvailabilityModel model)
        {
            var dbModel = _dbContext.Availabilities.Include("Location").SingleOrDefault(x => x.Id == model.Id);

            // TODO: Check edited dates are not in conflict with another availability
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
                if(dbModel.Location.Id != model.LocationId)
                {
                    var location = _dbContext.Locations.SingleOrDefault(x => x.Id == model.LocationId);
                    dbModel.Location = location;
                }

                _dbContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var dbModel = _dbContext.Availabilities.SingleOrDefault(x => x.Id == id);

            _dbContext.Availabilities.Remove(dbModel);
            _dbContext.SaveChanges();
        }

        public AvailabilityModel GetAvailability(int? id)
        {
            var dbModel = (from availability in _dbContext.Availabilities
                           join location in _dbContext.Locations
                              on availability.Location.Id equals location.Id
                           join vehicle in _dbContext.Vehicles
                              on availability.Vehicle.Id equals vehicle.Id
                           where availability.Id == id
                           select new AvailabilityModel
                           {
                               Id = availability.Id,
                               DateFrom = availability.DateFrom,
                               DateTo = availability.DateTo,
                               FullLocationName = location.CountryName + separ
                                + location.CityName + separ
                                + location.PostalCode + separ
                                + location.StreetName + space
                                + location.BuildingNumber + "/"
                                + location.OfficeNumber,
                               FullVehicleName = vehicle.Brand + space
                                + vehicle.ModelName,
                               LocationId = location.Id,
                               VehicleId = vehicle.Id
                           }).SingleOrDefault();

            return dbModel;
        }
    }
}
