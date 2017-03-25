using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Business.Models
{
    public class LocationModel
    {
        public int Id { get; set; }
        public String CountryName { get; set; }
        public String CityName { get; set; }
        public String StreetName { get; set; }
        public String PostalCode { get; set; }
        public int BuildingNumber { get; set; }
        public int OfficeNumber { get; set; }
    }
}
