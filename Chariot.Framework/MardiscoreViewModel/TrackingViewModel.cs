using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.MardiscoreViewModel
{
 public   class TrackingViewModel
    {
        public double GeoLength { set; get; }
        public double Geolatitude { set; get; }
        public double GeoAccuracy { set; get; }
        public DateTime LastDate { set; get; }
        public string NameAccount { set; get; }
        public string Namecampaign { set; get; }
        public string IdDevice  { set; get; }
        public int battery_level { set; get; }
    }
}
