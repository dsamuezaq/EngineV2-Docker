using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.MardisClientRestModel
{
   public class PostDevolucionFactura
    {
        public double d_DEVOLUCION { get; set; }
        public double d_ORDEN { get; set; }
        public double d_FECHA { get; set; }
        public double d_FACTURA { get; set; }
        public double d_CLIENTE { get; set; }
        public string d_PRODUCTO { get; set; }
        public double d_PRECIO { get; set; }
        public double d_CANTIDAD { get; set; }
        public double d_VENDEDOR { get; set; }
        public double d_ESTADO { get; set; }
        public string d_PEDIDO_MARDIS { get; set; }
    }
}
