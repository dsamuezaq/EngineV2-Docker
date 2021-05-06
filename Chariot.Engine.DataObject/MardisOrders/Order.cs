using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisOrders
{
    [Table("Pedidos", Schema = "MardisOrders")]
   public class Order
    {
        public Order()
        {
            this.pedidosItems = new HashSet<OrderDetail>();
        }
        [Key]
        public int id { get; set; }
        public string codCliente { get; set; }
        public string fecha { get; set; }
        public string idVendedor { get; set; }
        public string idpedidoExterno { get; set; }
        public Nullable<decimal> totalNeto { get; set; }
        public Nullable<decimal> totalFinal { get; set; }
        public Nullable<decimal> transferido { get; set; }
        public Nullable<decimal> gpsX { get; set; }
        public Nullable<decimal> gpsY { get; set; }
        public string p_PEDIDO_MARDIS { get; set; }
        public int? Idaccount { get; set; }
        public virtual ICollection<OrderDetail> pedidosItems { get; set; }
    }
}
