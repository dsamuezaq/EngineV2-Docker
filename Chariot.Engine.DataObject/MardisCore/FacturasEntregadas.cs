using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisCore
{
    [Table("FacturasEntregadas", Schema = "MardisOrders")]
    public class FacturasEntregadas
    {
        [Key]
        public int Id { get; set; }
        public string cO_CODCLI  { get; set; }
        public int cO_FACTURA { get; set; }
}
}
