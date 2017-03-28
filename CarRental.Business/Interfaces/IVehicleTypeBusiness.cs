using CarRental.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Business.Interfaces
{
    public interface IVehicleTypeBusiness
    {
        List<VehicleTypeModel> GetAll(bool showAll);
        void Add(VehicleTypeModel model);
        void Edit(VehicleTypeModel model);
        VehicleTypeModel GetVehicleType(int? id);
    }
}
