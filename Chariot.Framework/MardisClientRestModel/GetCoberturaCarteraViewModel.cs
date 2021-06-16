using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.MardisClientRestModel
{
  public  class GetCoberturaCarteraViewModel
    {
        public int codcli { get; set; }
        public string nombrecliente { get; set; }
        public int f_FACTURA { get; set; }
        public int f_DESPACHO { get; set; }
        public string tpd { get; set; }
        public int line { get; set; }
        public int nrodocumento { get; set; }
        public string tipodesp { get; set; }
        public int f_VENCIMIENTO { get; set; }
        public Double? valor { get; set; }
        public int codvendedor { get; set; }
        public string nombrevendedor { get; set; }
        public string formA_PAGO { get; set; }
        
    }
}
