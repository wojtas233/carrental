using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRental.WebApp.Areas.AdminPanel.Models
{
    public class AvailabilityDetailsViewModel
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateFrom { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateTo { get; set; }
        public int LocationId { get; set; }
        public int VehicleId { get; set; }

        [Display(Name = "Location")]
        public string FullLocationName { get; set; }
        [Display(Name = "Vehicle")]
        public string FullVehicleName { get; set; }
        public bool IsEditMode { get; set; }
    }
}