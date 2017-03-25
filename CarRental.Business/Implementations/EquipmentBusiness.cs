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
    public class EquipmentBusiness : IEquipmentBusiness
    {
        private readonly IApplicationDbContext _dbContext;
        public EquipmentBusiness(IApplicationDbContext db)
        {
            _dbContext = db;
        }

        public List<EquipmentModel> GetAll()
        {
            var model = _dbContext.Equipments.Select(x => new EquipmentModel
            {
                Id = x.Id,
                IsEnabled = x.IsEnabled,
                Name = x.Name,
                Price = x.Price
            }).ToList();
            return model;
        }

        public List<EquipmentListItemModel> GetListItems()
        {
            var model = _dbContext.Equipments.Select(x => new EquipmentListItemModel
            {
                Id = x.Id,
                Name = x.Name + ", " + x.Price
            }).ToList();
            return model;
        }
        
        public void Add(EquipmentModel model)
        {
            var dbModel = new Equipment
            {
                Id = model.Id,
                IsEnabled = model.IsEnabled,
                Name = model.Name,
                Price = model.Price
            };
            _dbContext.Equipments.Add(dbModel);
            _dbContext.SaveChanges();
        }

        public void Edit(EquipmentModel model)
        {
            var dbModel = _dbContext.Equipments.SingleOrDefault(x => x.Id == model.Id);

            if (dbModel != null)
            {
                if (dbModel.IsEnabled != model.IsEnabled)
                {
                    dbModel.IsEnabled = model.IsEnabled;
                }
                if (dbModel.Name != model.Name)
                {
                    dbModel.Name = model.Name;
                }
                if (dbModel.Price != model.Price)
                {
                    dbModel.Price = model.Price;
                }

                _dbContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var dbModel = _dbContext.Equipments.SingleOrDefault(x => x.Id == id);
            _dbContext.Equipments.Remove(dbModel);

            _dbContext.SaveChanges();
        }

        public EquipmentModel GetEquipment(int? id)
        {
            var dbModel = (from equipment in _dbContext.Equipments
                           where equipment.Id == id
                           select new EquipmentModel
                           {
                               Id = equipment.Id,
                               IsEnabled = equipment.IsEnabled,
                               Name = equipment.Name,
                               Price = equipment.Price
                           }).SingleOrDefault();

            return dbModel;
        }
    }
}
