using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DataAccess.Entities
{
    public class VehicleImage
    {
        public int Id { get; set; }
        public virtual Image Image { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
