using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.MardiscoreViewModel
{
  public  class GetTrackingViewModel
    {
        public int Idcampaign { get; set; }
        public string Iduser { get; set; }
        public DateTime DateTracking { get; set; }

        public string Status { get; set; }
    }
}
