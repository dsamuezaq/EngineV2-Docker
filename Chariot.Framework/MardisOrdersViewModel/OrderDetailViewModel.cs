using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.MardisOrdersViewModel
{
   public class OrderDetailViewModel
    {
        public int id { get; set; }
        public int idPedido { get; set; }
        public string idArticulo { get; set; }
        public Nullable<decimal> cantidad { get; set; } = 0;
        public Nullable<decimal> importeUnitario { get; set; } = 0;
        public Nullable<decimal> porcDescuento { get; set; } = 0;
        public Nullable<decimal> total { get; set; } = 0;
        public Nullable<int> transferido { get; set; } = 0;
        public Nullable<decimal> ppago { get; set; } = 0;
        public Nullable<decimal> nespecial { get; set; } = 0;
        public string num_factura { get; set; } = "Na";
        public string numero_factura { get; set; }  = "Na";
        public string formapago { get; set; } = "Na";
        public string unidad { get; set; } = "Na";
    }
}
