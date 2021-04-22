using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.SurtiApp
{
   public class EntregadorModeloApp
    {
        public List<EntregadorDetalle> entregadores { get; set; } = new List<EntregadorDetalle>();
    }

    public class EntregadorDetalle
    {
        public string username { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public bool active { get; set; }
        public bool deleted { get; set; }
        public string status { get; set; }
        public string delivery_count { get; set; }
        public int id { get; set; }
    }
}
