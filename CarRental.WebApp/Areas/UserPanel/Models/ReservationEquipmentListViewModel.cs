using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.WebApp.Areas.UserPanel.Models
{
    public class ReservationEquipmentListViewModel
    {
        public int ReservationId { get; set; }
        public List<ReservationEquipmentListItemVM> Equipments { get; set; }
    }
}