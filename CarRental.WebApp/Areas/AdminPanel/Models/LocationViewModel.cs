using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRental.WebApp.Areas.AdminPanel.Models
{
    public class LocationViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Country name")]
        public String CountryName { get; set; }
        [Display(Name = "City name")]
        public String CityName { get; set; }
        [Display(Name = "Street name")]
        public String StreetName { get; set; }
        [Display(Name = "Postal code")]
        public String PostalCode { get; set; }
        [Display(Name = "Building number")]
        public int BuildingNumber { get; set; }
        [Display(Name = "Office number")]
        public int OfficeNumber { get; set; }

        public string FullName
        {
            get
            {
                var separ = ", ";
                var space = " ";
                return this.CountryName + separ
                    + CityName + separ
                    + PostalCode + separ
                    + StreetName + space
                    + BuildingNumber + "/"
                    + OfficeNumber;
            }
            set
            {
            }
        }
    }
}