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
    public class VehicleTypeBusiness : IVehicleTypeBusiness
    {
        private readonly IApplicationDbContext _dbContext;
        public VehicleTypeBusiness(IApplicationDbContext db)
        {
            _dbContext = db;
        }

        public List<VehicleTypeModel> GetAll()
        {
            var model = (from vehicleType in _dbContext.VehicleTypes
                           join image in _dbContext.Images
                            on vehicleType.Image.Id equals image.Id
                           select new VehicleTypeModel
                           {
                               Id = vehicleType.Id,
                               Name = vehicleType.Name,
                               Image = new ImageModel
                               {
                                   Id = image.Id,
                                   Path = image.Path
                               }
                           }).ToList();
            return model;
        }

        public void Add(VehicleTypeModel model)
        {
            var generatedPath = ImageHelper.SaveToFolder(model.ImageFile, EnitityTypesEnum.VehicleType, model.Id.ToString(), model.Name);
            var image = new Image
            {
                Path = generatedPath
            };

            var dbModel = new VehicleType
            {
                Id = model.Id,
                Name = model.Name,
                Image = image
            };

            var createdModel = _dbContext.VehicleTypes.Add(dbModel);
            _dbContext.SaveChanges();
        }

        public void Edit(VehicleTypeModel model)
        {
            var dbModel = _dbContext.VehicleTypes.Include("Image").SingleOrDefault(x => x.Id == model.Id);

            if (dbModel != null)
            {
                if (dbModel.Name != model.Name)
                {
                    dbModel.Name = model.Name;
                }
                if(!string.IsNullOrEmpty(model.ImageFile.FileName) && dbModel.Image != null && model.Image != null)
                {
                    var generatedPath = ImageHelper.SaveToFolder(model.ImageFile, EnitityTypesEnum.VehicleType, model.Id.ToString(), model.Name);
                    if(dbModel.Image.Path != generatedPath)
                    {
                        ImageHelper.DeleteFromFolder(dbModel.Image.Path);
                        var image = new Image
                        {
                            Id = model.Image.Id,
                            Path = generatedPath
                        };
                        _dbContext.Images.Remove(dbModel.Image);
                        dbModel.Image = image;
                    }
                }
                
                _dbContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var dbModel = _dbContext.VehicleTypes.Include("Image").SingleOrDefault(x => x.Id == id);

            var image = _dbContext.Images.SingleOrDefault(x => x.Id == dbModel.Image.Id);
            if(image != null)
            {
                ImageHelper.DeleteFromFolder(image.Path);
                _dbContext.Images.Remove(image);
            }

            _dbContext.VehicleTypes.Remove(dbModel);
            _dbContext.SaveChanges();
        }

        public VehicleTypeModel GetVehicleType(int? id)
        {
            var dbModel = (from vehicleType in _dbContext.VehicleTypes
                           join image in _dbContext.Images
                            on vehicleType.Image.Id equals image.Id
                           where vehicleType.Id == id
                           select new VehicleTypeModel
                           {
                               Id = vehicleType.Id,
                               Name = vehicleType.Name,
                               Image = new ImageModel
                               {
                                   Id = image.Id,
                                   Path = image.Path
                               }
                           }).SingleOrDefault();

            return dbModel;
        }
    }
}
