using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.MardiscoreViewModel
{
  public  class GetBranchViewModel
    {
        public int Idpollster { get; set; }
        public int Idcampaign { get; set; }
        public DateTime DateTracking { get; set; }

        public string Status { get; set; }
    }
}
