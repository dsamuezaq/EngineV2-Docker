using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisCore
{
    [Table("PagoCartera", Schema = "MardisOrders")]
   public class PagoCartera
    {
        [Key]
        public int Id { get; set; }
        public int cO_ID { get; set; }
        public int cO_CODCLI { get; set; }
        public int cO_FACTURA { get; set; }
        public double cO_VALOR_COBRO { get; set; } = 0;
        public long cO_FECHA_COBRO { get; set; }
        public int cO_CODIGO_VEND { get; set; }
        public int cO_CAMION { get; set; }
        public int cO_ESTADO { get; set; }
        public DateTime cO_fecha { get; set; } = DateTime.Now.AddHours(-5);
    }
}
