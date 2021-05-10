using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisOrders
{
    [Table("Log_Cierre_Dia", Schema = "MardisOrders")]
    public  class Log_Cierre_Dia
    {
        [Key]
        public int idCierre { get; set; }
        public string idVendedor { get; set; }
        public DateTime? fecha_inicio { get; set; }
        public DateTime? fecha_cierre { get; set; }
        public string ruta { get; set; }
    }
}
