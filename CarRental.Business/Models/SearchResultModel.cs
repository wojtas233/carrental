using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Business.Models
{
    public class SearchResultModel
    {
        public int VehicleId { get; set; }
        public string VehicleName { get; set; }
        public int VehicleTypeId { get; set; }
        public string VehicleTypeName { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string ImagePath { get; set; }
        public int AvailabilityId { get; set; }
        public DateTime AvailabilityDateFrom { get; set; }
        public DateTime AvailabilityDateTo { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public SearchReservationDetailsModel Reservation { get; set; }
    }
}
