using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Business.Models
{
    public class AvailabilityModel
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateFrom { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateTo { get; set; }

        public int LocationId { get; set; }
        public int VehicleId { get; set; }

        public string FullLocationName { get; set; }
        public string FullVehicleName { get; set; }
    }
}
