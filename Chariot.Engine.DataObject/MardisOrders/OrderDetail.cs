using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisOrders
{
    [Table("Client", Schema = "MardisOrders")]
    public  class OrderDetail
    {
        [Key]
        public int id { get; set; }
        public int idPedido { get; set; }
        public string idArticulo { get; set; }
        public Nullable<decimal> cantidad { get; set; }
        public Nullable<decimal> importeUnitario { get; set; }
        public Nullable<decimal> porcDescuento { get; set; }
        public Nullable<decimal> total { get; set; }
        public Nullable<int> transferido { get; set; }
        public Nullable<decimal> ppago { get; set; }
        public Nullable<decimal> nespecial { get; set; }
        public string num_factura { get; set; }
        public string numero_factura { get; set; }
        public string formapago { get; set; }
        public string unidad { get; set; }
      
    }
}
