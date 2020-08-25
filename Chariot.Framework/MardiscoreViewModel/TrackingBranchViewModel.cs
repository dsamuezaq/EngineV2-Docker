using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.MardiscoreViewModel
{
   public class TrackingBranchViewModel
    {

        public string CodeBranch { get; set; }
        public string NameBranch { get; set; }
        public string StreetBranch { get; set; }
        public string StatusBranch { get; set; }
        public string RouteBranch { get; set; }
        public string IdDevice { get; set; }
        public string campaign { get; set; }
        public double? GeoLength { get; set; }
        public double? Geolatitude { get; set; }
        public int? idpollster { get; set; }
        public int? Idcampaign { get; set; }
        public DateTime datetime_tracking { get; set; }
        public double? timetaks { get; set; }
        public string dateexecini { get; set; }
        public string dateexec { get; set; }
    }
}
