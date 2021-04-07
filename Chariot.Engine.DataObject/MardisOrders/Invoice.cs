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
    /// Tabla de Facturas en el sistema
    /// </summary>
    [Table("INVOICE", Schema = "MardisOrders")]
    public class Invoice
    {
        [Key]
        public Int64 IDINVOICE { get; set; } = 0;
        public string NUMBER { get; set; }
        public DateTime FECHA { get; set; } = DateTime.Now;
        public string CLIENTE { get; set; }
        public virtual ICollection<Invoice_detail> Invoice_details { get; set; }

    }
}
