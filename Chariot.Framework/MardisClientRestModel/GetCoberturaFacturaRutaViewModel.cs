using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.MardisClientRestModel
{
   public class GetCoberturaFacturaRutaViewModel
    {
        public int factura { get; set; }
        public int fecha { get; set; }
        public int codigocliente { get; set; }
        public string codigoprod { get; set; }
        public string nombreprod { get; set; }
        public int cantidad { get; set; }
        public Double? precio { get; set; }
        public Double? subtotal { get; set; }
        public Double? iva { get; set; }
        public Double? total { get; set; }
        public int camion { get; set; }
        public string placa { get; set; }
        public int viaje { get; set; }
        public int codvend { get; set; }
        public string nombrevend { get; set; }
        public int pedido { get; set; }
        public string pedidomardis { get; set; }
        public string estadoentrega { get; set; }

    }
}
