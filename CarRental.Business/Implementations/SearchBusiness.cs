using CarRental.Business.Interfaces;
using CarRental.Business.Models;
using CarRental.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Business.Implementations
{
    public class SearchBusiness : ISearchBusiness
    {
        private readonly IApplicationDbContext _dbContext;
        public SearchBusiness(IApplicationDbContext db)
        {
            _dbContext = db;
        }

        public List<SearchResultModel> GetResults(SearchModel model)
        {
            string separ = ", ";
            string space = " ";

            var results = (from vehicle in _dbContext.Vehicles
                           join vehicleType in _dbContext.VehicleTypes
                              on vehicle.VehicleType.Id equals vehicleType.Id
                           join availability in _dbContext.Availabilities
                              on vehicle.Id equals availability.Vehicle.Id
                           join location in _dbContext.Locations
                              on availability.Location.Id equals location.Id
                           join vehicleImage in _dbContext.VehicleImages
                              on vehicle.Id equals vehicleImage.Vehicle.Id
                           join image in _dbContext.Images
                              on vehicleImage.Image.Id equals image.Id
                           where (availability.DateFrom < model.DateTo && model.DateFrom < availability.DateTo)
                              && model.VehicleTypeId == vehicleType.Id && model.LocationId == location.Id
                           select new SearchResultModel
                           {
                               VehicleId = vehicle.Id,
                               VehicleName = vehicle.Brand + space
                                    + vehicle.ModelName,
                               VehicleTypeId = vehicleType.Id,
                               VehicleTypeName = vehicleType.Name,
                               LocationId = location.Id,
                               LocationName = location.CountryName + separ
                                    + location.CityName + separ
                                    + location.PostalCode + separ
                                    + location.StreetName + space
                                    + location.BuildingNumber + "/"
                                    + location.OfficeNumber,
                               ImagePath = image.Path,
                               Reservation = new SearchReservationDetailsModel
                               {
                                   AvailabilityDateFrom = availability.DateFrom,
                                   AvailabilityDateTo = availability.DateTo,
                                   AvailabilityId = availability.Id,
                                   DateFrom = model.DateFrom,
                                   DateTo = model.DateTo,
                                   LocationName = location.CountryName + separ
                                    + location.CityName + separ
                                    + location.PostalCode + separ
                                    + location.StreetName + space
                                    + location.BuildingNumber + "/"
                                    + location.OfficeNumber
                               },
                               AvailabilityDateFrom = availability.DateFrom,
                               AvailabilityDateTo = availability.DateTo,
                               AvailabilityId = availability.Id,
                               DateFrom = model.DateFrom,
                               DateTo = model.DateTo
                           })
                           .GroupBy(x => x.VehicleId)
                           .Select(group => group.FirstOrDefault())
                           .ToList();

            return results;
        }
    }
}
