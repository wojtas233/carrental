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
    public class VehicleBusiness : IVehicleBusiness
    {
        private readonly IApplicationDbContext _dbContext;
        public VehicleBusiness(IApplicationDbContext db)
        {
            _dbContext = db;
        }

        public List<VehicleModel> GetAll()
        {
            var model = (from vehicle in _dbContext.Vehicles
                         join vehicleType in _dbContext.VehicleTypes
                            on vehicle.VehicleType.Id equals vehicleType.Id
                         select new VehicleModel
                         {
                             Id = vehicle.Id,
                             Brand = vehicle.Brand,
                             Description = vehicle.Description,
                             ModelName = vehicle.ModelName,
                             VehicleTypeId = vehicleType.Id,
                             VehicleTypeName = vehicleType.Name,
                             PricePerHour = vehicle.PricePerHour
                         }).ToList();

            return model;
        }

        public int Add(VehicleModel model)
        {
            var vehicleType = _dbContext.VehicleTypes.SingleOrDefault(x => x.Id == model.VehicleTypeId);
            var dbModel = new Vehicle
            {
                Id = model.Id,
                Brand = model.Brand,
                Description = model.Description,
                ModelName = model.ModelName,
                VehicleType = vehicleType,
                PricePerHour = model.PricePerHour
            };
            var createdModel = _dbContext.Vehicles.Add(dbModel); // I know
            _dbContext.SaveChanges();

            return createdModel.Id;
        }

        public void Edit(VehicleModel model)
        {
            var dbModel = _dbContext.Vehicles.Include("VehicleType").SingleOrDefault(x => x.Id == model.Id);

            if (dbModel != null)
            {
                if (dbModel.Brand != model.Brand)
                {
                    dbModel.Brand = model.Brand;
                }
                if (dbModel.ModelName != model.ModelName)
                {
                    dbModel.ModelName = model.ModelName;
                }
                if (dbModel.Description != model.Description)
                {
                    dbModel.Description = model.Description;
                }
                if (dbModel.PricePerHour != model.PricePerHour)
                {
                    dbModel.PricePerHour = model.PricePerHour;
                }
                if (dbModel.VehicleType.Id != model.VehicleTypeId)
                {
                    var vehicleType = _dbContext.VehicleTypes.SingleOrDefault(x => x.Id == model.VehicleTypeId);
                    dbModel.VehicleType = vehicleType;
                }

                _dbContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var dbModel = _dbContext.Vehicles.SingleOrDefault(x => x.Id == id);

            var availabilities = _dbContext.Availabilities.Include("Vehicle").Where(x => x.Vehicle.Id == dbModel.Id);
            if(availabilities != null && availabilities.Any())
            {
                _dbContext.Availabilities.RemoveRange(availabilities);
            }

            var imagesConnections = _dbContext.VehicleImages.Include("Vehicle").Include("Image").Where(x => x.Vehicle.Id == dbModel.Id); // Get images connections
            var imagesIds = imagesConnections.Select(x => x.Image.Id); // Get images ids to delete
            var images = _dbContext.Images.Where(x => imagesIds.Contains(x.Id)); // Get images to delete

            if (imagesConnections != null && imagesConnections.Any())
            {
                _dbContext.VehicleImages.RemoveRange(imagesConnections); // Delete images connections
            }

            if (images != null && images.Any())
            {
                _dbContext.Images.RemoveRange(images); // Delete images
            }

            _dbContext.Vehicles.Remove(dbModel);
            _dbContext.SaveChanges();
        }

        public VehicleModel GetVehicle(int? id)
        {
            var model = (from vehicle in _dbContext.Vehicles
                         join vehicleType in _dbContext.VehicleTypes
                            on vehicle.VehicleType.Id equals vehicleType.Id
                         where vehicle.Id == id
                         select new VehicleModel
                         {
                             Id = vehicle.Id,
                             Brand = vehicle.Brand,
                             Description = vehicle.Description,
                             ModelName = vehicle.ModelName,
                             VehicleTypeId = vehicleType.Id,
                             VehicleTypeName = vehicleType.Name,
                             PricePerHour = vehicle.PricePerHour
                         }).SingleOrDefault();
            return model;
        }
    }
}
