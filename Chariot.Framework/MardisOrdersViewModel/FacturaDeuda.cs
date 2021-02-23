using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.MardisOrdersViewModel
{
   public class FacturaDeuda
    {
        public double? total { get; set; }
        public double? Pagos { get; set; } = 0;
        public int   numeroFactura { get; set; }

        public int codigoCliente { get; set; }

        public string EstadoFactura { get; set; }
        public string codigoVendedor { get; set; }

        public DateTime  Fecha { get; set; }

    }
}
