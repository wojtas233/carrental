using CarRental.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Business.Interfaces
{
    public interface IAvailabilityBusiness
    {
        List<AvailabilityModel> GetAll(int? vehicleId);
        bool Add(AvailabilityModel model);
        void Edit(AvailabilityModel model);
        void Delete(int id);
        AvailabilityModel GetAvailability(int? id);
    }
}
