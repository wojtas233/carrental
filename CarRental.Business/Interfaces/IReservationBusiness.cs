using CarRental.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Business.Interfaces
{
    public interface IReservationBusiness
    {
        List<ReservationDetailsModel> GetAll(string userId);
        bool Add(ReservationModel model);
        void Edit(ReservationModel model);
        void Delete(int id);
        ReservationDetailsModel GetReservation(int? id);
    }
}
