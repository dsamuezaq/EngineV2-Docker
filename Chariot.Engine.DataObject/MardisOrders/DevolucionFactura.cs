using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisOrders
{

    [Table("DevolucionFactura", Schema = "MardisOrders")]
    public class DevolucionFactura
    {
        [Key]
        public int Id { get; set; }
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
