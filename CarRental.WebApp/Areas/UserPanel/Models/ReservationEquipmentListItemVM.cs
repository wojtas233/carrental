using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.WebApp.Areas.UserPanel.Models
{
    public class ReservationEquipmentListItemVM : ReservationEquipmentViewModel
    {
        public string EquipmentName { get; set; }
        public decimal EquipmentPrice { get; set; }
    }
}