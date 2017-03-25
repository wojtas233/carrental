using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRental.WebApp.Areas.AdminPanel.Models
{
    public class VehicleImageViewModel
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        [Display(Name = "Image")]
        public string Path { get; set; }
        public bool IsEditMode { get; set; }
    }
}