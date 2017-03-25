using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRental.WebApp.Areas.AdminPanel.Models
{
    public class EquipmentViewModel
    {
        public EquipmentViewModel()
        {
            IsEnabled = true;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        [Display(Name = "Enabled")]
        public bool IsEnabled { get; set; }
    }
}