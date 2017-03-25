using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CarRental.Business.Models
{
    public class VehicleImageModel
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string Path { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}
