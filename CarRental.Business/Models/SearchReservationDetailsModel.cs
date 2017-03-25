using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Business.Models
{
    public class SearchReservationDetailsModel : ReservationModel
    {
        public string LocationName { get; set; }
        public DateTime AvailabilityDateFrom { get; set; }
        public DateTime AvailabilityDateTo { get; set; }
    }
}
