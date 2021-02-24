using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.MardisClientRestModel
{
   public class GetCoberturaFacturaDevolucion
    {
        public int dF_DEVOLUCION { get; set; }
        public int dF_ORDEN { get; set; }
        public int dF_FECHA { get; set; }
        public int dF_FACTURA { get; set; }
        public int dF_CLIENTE { get; set; }
        public String dF_NOMBRECLIENTE { get; set; }
        public String dF_PRODUCTO { get; set; }
        public String dF_NOMBREPRO { get; set; }
        public double dF_PRECIO { get; set; }
        public int dF_CANTIDAD { get; set; }
        public double dF_IVA { get; set; }
        public int dF_VENDEDOR { get; set; }
        public int dF_ESTADO { get; set; }
    }
}
