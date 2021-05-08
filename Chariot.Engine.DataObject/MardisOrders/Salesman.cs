using Chariot.Engine.DataObject.MardisSecurity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisOrders
{
    [Table("Salesman", Schema = "MardisOrders")]
    public  class Salesman
    {
        [Key]
        public int id { get; set; }
        public string idVendedor { get; set; }
         public string nombre { get; set; }
         public string codigoDeValidacion { get; set; }
        public string dispositivo { get; set; }
        public string estado { get; set; }
        public int? idaccount { get; set; }
        public Guid? Iduser { get; set; }
        [ForeignKey("Iduser")]
        public virtual User user { get; set; }

    }
}
