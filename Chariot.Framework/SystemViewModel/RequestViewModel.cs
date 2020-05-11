using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.SystemViewModel
{
    public class RequestViewModel
    {
        public string token { get; set; }
        public string Iduser { get; set; }
        public object data { get; set; }
        public string dataJsonString { get; set; }

    }
}
