using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.MardiscoreViewModel
{
    public class TrackingDashboardModelReply
    {
        public int Total_business { get; set; }
        public int Full { get; set; }
        public int Incompletes { get; set; }
        public int Active_pollsters { get; set; }
        public int Total_pollsters { get; set; }
        public int Delay  { get; set; }
        public int Medium { get; set; }
        public int Regular { get; set; }


    }
}
