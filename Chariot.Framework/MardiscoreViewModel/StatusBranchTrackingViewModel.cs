using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.MardiscoreViewModel
{
   public class StatusBranchTrackingViewModel
    {
        public string IdDevice{ get; set; }
        public string campaign { get; set; }
        public string Code { get; set; }
        public string AggregateUri { get; set; }
        public string Status { get; set; }
        public double TimeTask { get; set; }
  
    }
}
