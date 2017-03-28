using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DataAccess.Entities
{
    public class VehicleType
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public Image Image { get; set; }
        public bool IsEnabled { get; set; }
    }
}
