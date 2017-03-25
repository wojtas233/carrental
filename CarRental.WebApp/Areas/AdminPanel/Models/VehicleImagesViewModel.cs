using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.WebApp.Areas.AdminPanel.Models
{
    public class VehicleImagesViewModel
    {
        public int VehicleId { get; set; }
        public List<VehicleImageViewModel> Images { get; set; }
        public bool IsEditMode { get; set; }
    }
}