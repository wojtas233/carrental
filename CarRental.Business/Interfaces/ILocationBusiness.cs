using CarRental.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Business.Interfaces
{
    public interface ILocationBusiness
    {
        List<LocationModel> GetAll();
        List<LocationSearchModel> GetNames();
        void Add(LocationModel model);
        void Edit(LocationModel model);
        void Delete(int id);
        LocationModel GetLocation(int? id);
    }
}
