using CarRental.Business.Models;
using CarRental.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Business.Interfaces
{
    public interface IVehicleBusiness
    {
        List<VehicleModel> GetAll();
        int Add(VehicleModel model);
        void Edit(VehicleModel model);
        void Delete(int id);
        VehicleModel GetVehicle(int? id);
    }
}
