using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.MardisOrdersViewModel
{
    public class CarteraPagoViewModel
    {

        public int cO_ID { get; set; }
        public int cO_CODCLI { get; set; }
        public int cO_FACTURA { get; set; }
        public double cO_VALOR_COBRO { get; set; }
        public long cO_FECHA_COBRO { get; set; }
        public int cO_CODIGO_VEND { get; set; }
        public int cO_CAMION { get; set; }
        public int cO_ESTADO { get; set; }
    }
}
