using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.MardisBranchViewModel
{
    public class MapaLocalModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string codigo { get; set; }
        public string Region { get; set; }
        public string type { get; set; }
        public int SpeedLimit { get; set; }
        public double lat { get; set; }
        public double Lng { get; set; }
    }   
}
