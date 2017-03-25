using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DataAccess.Entities
{
    public class Location
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
