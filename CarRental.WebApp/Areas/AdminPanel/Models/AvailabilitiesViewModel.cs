using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.WebApp.Areas.AdminPanel.Models
{
    public class AvailabilitiesViewModel
    {
        public int VehicleId { get; set; }
        public List<AvailabilityDetailsViewModel> Availabilities { get; set; }
        public bool IsEditMode { get; set; }
    }
}