using Chariot.Engine.DataObject.MardisOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.MardisOrdersViewModel
{
    public   class OrdersViewModel
    {
        public int id { get; set; }
        public string codCliente { get; set; }
        public string fecha { get; set; }
        public string idVendedor { get; set; }
        public Nullable<decimal> totalNeto { get; set; }
        public Nullable<decimal> totalFinal { get; set; }
        public Nullable<decimal> transferido { get; set; }
        public Nullable<decimal> gpsX { get; set; }
        public Nullable<decimal> gpsY { get; set; }
        public string p_PEDIDO_MARDIS { get; set; }
        public int? Idaccount { get; set; } = 15;
        public virtual ICollection<OrderDetail> pedidosItems { get; set; } = new List<OrderDetail>() ;
    }
}
