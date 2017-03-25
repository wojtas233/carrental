using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Business.Models
{
    public class VehicleModel
    {
        public int Id { get; set; }
        public String Brand { get; set; }
        public String ModelName { get; set; }
        public String Description { get; set; }
        public int VehicleTypeId { get; set; }
        public string VehicleTypeName { get; set; }
        public decimal PricePerHour { get; set; }
    }
}
