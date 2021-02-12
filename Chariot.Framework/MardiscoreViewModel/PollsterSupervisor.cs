using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.MardiscoreViewModel
{
    public class PollsterSupervisor
    {
        public int Idpollster { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public float latitud { get; set; }
        public float longitud { get; set; }
        public int bateria { get; set; }
        public DateTime ultima_conexion { get; set; }
        public DateTime inicio { get; set; }
        public DateTime fin { get; set; }
        public bool statusRoute { get; set; }
    }
}
