using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRental.WebApp.Models
{
    public class SearchResultViewModel
    {
        public int VehicleId { get; set; }

        [Display(Name = "Name")]
        public string VehicleName { get; set; }

        public int VehicleTypeId { get; set; }

        [Display(Name = "Type")]
        public string VehicleTypeName { get; set; }
        public int LocationId { get; set; }

        [Display(Name = "Location")]
        public string LocationName { get; set; }

        [Display(Name = "Image path")]
        public string ImagePath { get; set; }

        public int AvailabilityId { get; set; }

        public DateTime AvailabilityDateFrom { get; set; }
        public DateTime AvailabilityDateTo { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public ReservationDetailsViewModel Reservation { get; set; }
    }
}