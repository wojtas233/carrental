using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRental.WebApp.Areas.AdminPanel.Models
{
    public class AvailabilityViewModel
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateFrom { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DateTo { get; set; }
        [Display(Name = "Location")]
        public int LocationId { get; set; }
        public List<LocationViewModel> Locations { get; set; }
        public int VehicleId { get; set; }
        public bool IsEditMode { get; set; }
    }
}