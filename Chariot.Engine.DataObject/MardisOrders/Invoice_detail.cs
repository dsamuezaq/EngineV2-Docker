using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisOrders
{
    /// <summary>
    /// Tabla de Detalle Facturas en el sistema
    /// </summary>
    [Table("INVOICE_DETAIL", Schema = "MardisOrders")]
    public class Invoice_detail
    {
        [Key]
        public Int64 ID_INVOICE_DETAIL { get; set; } = 0;
        [ForeignKey("id")]
        public int IDPRODUCTO { get; set; }
        public Int64 IDINVOICE { get; set; }
        [ForeignKey("IDINVOICE")]
        public virtual Invoice invoice { get; set; }
        public float AMOUNT { get; set; }
        public string DESCRIPTION { get; set; }
        public float UNIT_PRICE { get; set; }
        public float IVA { get; set; }

    }
}
