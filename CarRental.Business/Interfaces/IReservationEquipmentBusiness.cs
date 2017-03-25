using CarRental.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Business.Interfaces
{
    public interface IReservationEquipmentBusiness
    {
        List<ReservationEquipmentModel> GetAll(int reservationId);
        void Add(ReservationEquipmentModel model);
        void Delete(int id);
        ReservationEquipmentModel GetReservationEquipment(int? id);
    }
}
