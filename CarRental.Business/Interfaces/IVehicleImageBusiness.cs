using CarRental.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Business.Interfaces
{
    public interface IVehicleImageBusiness
    {
        List<VehicleImageModel> GetAll(int vehicleId);
        void Add(VehicleImageModel model);
        void Edit(VehicleImageModel model);
        void Delete(int id);
        VehicleImageModel GetImage(int? id);
    }
}
