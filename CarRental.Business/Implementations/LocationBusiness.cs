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
    public class LocationBusiness : ILocationBusiness
    {
        private readonly IApplicationDbContext _dbContext;
        public LocationBusiness(IApplicationDbContext db)
        {
            _dbContext = db;
        }

        public List<LocationModel> GetAll()
        {
            var model = _dbContext.Locations.Select(x => new LocationModel
            {
                Id = x.Id,
                CityName = x.CityName,
                CountryName = x.CountryName,
                StreetName = x.StreetName,
                PostalCode = x.PostalCode,
                BuildingNumber = x.BuildingNumber,
                OfficeNumber = x.OfficeNumber
            }).ToList();
            return model;
        }

        public List<LocationSearchModel> GetNames()
        {
            string separ = ", ";
            string space = " ";
            var model = _dbContext.Locations.Select(x => new LocationSearchModel
                {
                    Id = x.Id,
                    FullName = x.CountryName + separ
                            + x.CityName + separ
                            + x.PostalCode + separ
                            + x.StreetName + space
                            + x.BuildingNumber + "/"
                            + x.OfficeNumber,
            }).ToList();

            return model;
        }

        public void Add(LocationModel model)
        {
            var dbModel = new Location
            {
                Id = model.Id,
                CityName = model.CityName,
                CountryName = model.CountryName,
                StreetName = model.StreetName,
                PostalCode = model.PostalCode,
                BuildingNumber = model.BuildingNumber,
                OfficeNumber = model.OfficeNumber
            };
            _dbContext.Locations.Add(dbModel);
            _dbContext.SaveChanges();
        }

        public void Edit(LocationModel model)
        {
            var dbModel = _dbContext.Locations.SingleOrDefault(x => x.Id == model.Id);

            if (dbModel != null)
            {
                if (dbModel.CountryName != model.CountryName)
                {
                    dbModel.CountryName = model.CountryName;
                }
                if (dbModel.CityName != model.CityName)
                {
                    dbModel.CityName = model.CityName;
                }
                if (dbModel.BuildingNumber != model.BuildingNumber)
                {
                    dbModel.BuildingNumber = model.BuildingNumber;
                }
                if (dbModel.OfficeNumber != model.OfficeNumber)
                {
                    dbModel.OfficeNumber = model.OfficeNumber;
                }
                if (dbModel.PostalCode != model.PostalCode)
                {
                    dbModel.PostalCode = model.PostalCode;
                }
                if (dbModel.StreetName != model.StreetName)
                {
                    dbModel.StreetName = model.StreetName;
                }

                _dbContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            // TODO: Remove all reservations for availabilities
            var availabilities = _dbContext.Availabilities.Include("Location").Where(x => x.Location.Id == id);
            if(availabilities != null && availabilities.Any())
            {
                _dbContext.Availabilities.RemoveRange(availabilities);
            }

            var dbModel = _dbContext.Locations.SingleOrDefault(x => x.Id == id);
            _dbContext.Locations.Remove(dbModel);

            _dbContext.SaveChanges();
        }

        public LocationModel GetLocation(int? id)
        {
            var dbModel = (from location in _dbContext.Locations
                           where location.Id == id
                           select new LocationModel
                           {
                               Id = location.Id,
                               CityName = location.CityName,
                               CountryName = location.CountryName,
                               StreetName = location.StreetName,
                               PostalCode = location.PostalCode,
                               BuildingNumber = location.BuildingNumber,
                               OfficeNumber = location.OfficeNumber
                           }).SingleOrDefault();

            return dbModel;
        }

    }
}
