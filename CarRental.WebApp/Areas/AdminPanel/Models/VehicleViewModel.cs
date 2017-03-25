using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRental.WebApp.Areas.AdminPanel.Models
{
    public class VehicleViewModel
    {
        public VehicleViewModel()
        {
            VehicleTypes = new List<VehicleTypeViewModel>();
            IsSuccessModified = false;
            PricePerHour = 100;
        }
        public int Id { get; set; }
        public String Brand { get; set; }
        [Display(Name = "Model name")]
        public String ModelName { get; set; }
        public String Description { get; set; }

        public int VehicleTypeId { get; set; }
        [Display(Name = "Vehicle type")]
        public string VehicleTypeName { get; set; }
        public List<VehicleTypeViewModel> VehicleTypes { get; set; }

        [Display(Name = "Price per hour")]
        public decimal PricePerHour { get; set; }
        public bool IsSuccessModified { get; set; }
    }
}