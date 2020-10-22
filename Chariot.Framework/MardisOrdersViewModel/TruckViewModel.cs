using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.MardisOrdersViewModel
{
   public class TruckViewModel
    {
        public int truck_numbre { get; set; }
        public string truck_plate { get; set; }
        public List<TruckDetailViewModel> TruckDetails { get; set; }
    }
}
