using CarRental.Business.Helpers;
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
    public class VehicleImageBusiness : IVehicleImageBusiness
    {
        private readonly IApplicationDbContext _dbContext;
        public VehicleImageBusiness(IApplicationDbContext db)
        {
            _dbContext = db;
        }

        public List<VehicleImageModel> GetAll(int vehicleId)
        {
            var model = (from image in _dbContext.Images
                         join vehicleImage in _dbContext.VehicleImages
                            on image.Id equals vehicleImage.Image.Id
                         join vehicle in _dbContext.Vehicles
                            on vehicleImage.Vehicle.Id equals vehicle.Id
                         where vehicle.Id == vehicleId
                         select new VehicleImageModel
                         {
                             Id = image.Id,
                             VehicleId = vehicle.Id,
                             Path = image.Path
                         }).ToList();

            return model;
        }

        public void Add(VehicleImageModel model)
        {
            var generatedPath = ImageHelper.SaveToFolder(model.ImageFile, EnitityTypesEnum.Vehicle, model.Id.ToString(), model.VehicleId.ToString());
            var image = new Image
            {
                Path = generatedPath
            };

            var vehicle = _dbContext.Vehicles.SingleOrDefault(x => x.Id == model.VehicleId);

            var dbModel = new VehicleImage
            {
                Id = model.Id,
                Image = image,
                Vehicle = vehicle
            };

            var createdModel = _dbContext.VehicleImages.Add(dbModel); // I know
            _dbContext.SaveChanges();
        }

        public void Edit(VehicleImageModel model)
        {
            var dbModel = _dbContext.Images.SingleOrDefault(x => x.Id == model.Id);

            if (dbModel != null && !string.IsNullOrEmpty(model.ImageFile.FileName))
            {
                ImageHelper.DeleteFromFolder(dbModel.Path);
                var generatedPath = ImageHelper.SaveToFolder(model.ImageFile, EnitityTypesEnum.Vehicle, model.Id.ToString(), model.VehicleId.ToString());
                dbModel.Path = generatedPath;

                _dbContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var vehicleImage = _dbContext.VehicleImages.Include("Image").SingleOrDefault(x => x.Image.Id == id);
            if(vehicleImage != null) {
                _dbContext.VehicleImages.Remove(vehicleImage); // Remove connection
            }

            var dbModel = _dbContext.Images.SingleOrDefault(x => x.Id == id);
            if(dbModel != null)
            {
                _dbContext.Images.Remove(dbModel); // Remove image
            }

            _dbContext.SaveChanges();
        }

        public VehicleImageModel GetImage(int? id)
        {
            var model = (from image in _dbContext.Images
                         join vehicleImage in _dbContext.VehicleImages
                            on image.Id equals vehicleImage.Image.Id
                         join vehicle in _dbContext.Vehicles
                             on vehicleImage.Vehicle.Id equals vehicle.Id
                         where image.Id == id
                         select new VehicleImageModel
                         {
                             Id = image.Id,
                             VehicleId = vehicle.Id,
                             Path = image.Path
                         }).SingleOrDefault();
            return model;
        }
    }
}
