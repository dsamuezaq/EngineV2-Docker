using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.MardiscoreViewModel
{
   public class BranchesModelReply
    {
        public string CodeBranch { get; set; }
        public string NameBranch { get; set; }
        public string StreetBranch { get; set; }
        public string StatusBranch { get; set; }
        public double? TimeTask { get; set; }
        public string RouteBranch { get; set; }
        public double? GeoLength { get; set; }
        public double? Geolatitude { get; set; }
        public string Status { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string uri { get; set; }
    }
}
