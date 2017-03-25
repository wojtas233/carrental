using CarRental.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Business.Interfaces
{
    public interface IEquipmentBusiness
    {
        List<EquipmentModel> GetAll();
        List<EquipmentListItemModel> GetListItems();
        void Add(EquipmentModel model);
        void Edit(EquipmentModel model);
        void Delete(int id);
        EquipmentModel GetEquipment(int? id);
    }
}
