using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRental.WebApp.Areas.UserPanel.Models
{
    public class ReservationEquipmentViewModel
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }

        [Display(Name="Equipment")]
        public int EquipmentId { get; set; }
    }
}