using Chariot.Engine.DataObject.MardisCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.SystemViewModel
{
 public   class ReplyViewModelPollsters
    {
        public string status { get; set; }
        public string messege { get; set; }
        public string error { get; set; }
        public List<Pollster> data { get; set; }
    }
}
