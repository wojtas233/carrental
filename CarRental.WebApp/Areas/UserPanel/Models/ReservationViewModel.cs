using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRental.WebApp.Areas.UserPanel.Models
{
    public class ReservationViewModel
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateFrom { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateTo { get; set; }
        public int AvailabilityId { get; set; }
    }
}