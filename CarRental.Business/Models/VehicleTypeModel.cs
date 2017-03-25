using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CarRental.Business.Models
{
    public class VehicleTypeModel
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public ImageModel Image { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}
