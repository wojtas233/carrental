using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DataAccess.Entities
{
    public class ReservationEquipment
    {
        public int Id { get; set; }

        public virtual Reservation Reservation { get; set; }
        public virtual Equipment Equipment { get; set; }
    }
}
