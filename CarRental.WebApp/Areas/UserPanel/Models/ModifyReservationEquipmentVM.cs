using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.WebApp.Areas.UserPanel.Models
{
    public class ModifyReservationEquipmentVM : ReservationEquipmentViewModel
    {
        public List<EquipmentViewModel> Equipments { get; set; }

    }
}