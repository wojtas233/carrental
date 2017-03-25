using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DataAccess.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }
        public String Brand { get; set; }
        public String ModelName { get; set; }
        public String Description { get; set; }
        public decimal PricePerHour { get; set; }

        public virtual VehicleType VehicleType { get; set; }
    }
}
