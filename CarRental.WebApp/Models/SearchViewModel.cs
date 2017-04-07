using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRental.WebApp.Models
{
    public class SearchViewModel
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Pick up")]
        public DateTime DateFrom { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Drop off")]
        public DateTime DateTo { get; set; }
        [Display(Name = "Type")]
        public int VehicleTypeId { get; set; }
        public List<VehicleTypeViewModel> VehicleTypes { get; set; }
        [Display(Name = "Location")]
        public int LocationId { get; set; }
        public List<LocationViewModel> Locations { get; set; }
    }
}