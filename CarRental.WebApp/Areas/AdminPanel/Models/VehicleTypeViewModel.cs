using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.WebApp.Areas.AdminPanel.Models
{
    public class VehicleTypeViewModel
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public bool IsEnabled { get; set; }
        public ImageViewModel Image { get; set; }
    }
}