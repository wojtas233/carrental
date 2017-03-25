using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.WebApp.Models
{
    public class VehicleViewModel
    {
        public int Id { get; set; }
        public String Brand { get; set; }
        public String ModelName { get; set; }
        public String Description { get; set; }

        public int VehicleTypeId { get; set; }
    }
}