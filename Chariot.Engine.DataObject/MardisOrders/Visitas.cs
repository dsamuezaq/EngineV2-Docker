using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisOrders
{
    [Table("visitasUio", Schema = "MardisOrders")]
    public class Visitas
    {
        [Key]
        public int Id { get; set; }
        public string codcliente { get; set; }
        public string codvendedor { get; set; }
        public string fechavisita { get; set; }
        public decimal? Latitud { get; set; }
        public decimal? Longitud { get; set; }
        public string Linkfotoexterior { get; set; }
        public string Compro { get; set; }
        public string Observacion { get; set; }

    }
}
