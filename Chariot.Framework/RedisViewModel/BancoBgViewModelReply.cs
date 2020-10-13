using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.RedisViewModel
{
  public  class BancoBgViewModelReply
    {
        public int id { get; set; }
        public string idbg { get; set; }
        public string tipo { get; set; }
        public string nombreLocal { get; set; }
        public string propietario { get; set; }
        public string direccion { get; set; }
        public string cedula { get; set; }
        public string tiponegocio { get; set; }
        public string provincia { get; set; }
        public string ciudad { get; set; }
        public string parroquia { get; set; }
        public string trmOfficia { get; set; }
        public string trmSupervi { get; set; }
        public string telefono { get; set; }
        public string codigomardis { get; set; }
        public Nullable<double> latitud { get; set; }
        public Nullable<double> longitud { get; set; }
        public string digitador { get; set; }
        public string banner { get; set; }
        public Nullable<System.DateTime> fecha_Modificacion { get; set; }
        public string estado { get; set; }
        public string horarioLV { get; set; }
        public string horarioSD { get; set; }
        public string horarioS { get; set; }
        public string horarioD { get; set; }
    }
}
