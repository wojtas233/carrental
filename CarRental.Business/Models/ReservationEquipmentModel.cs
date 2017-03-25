using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Business.Models
{
    public class ReservationEquipmentModel
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public decimal EquipmentPrice { get; set; }
    }
}
