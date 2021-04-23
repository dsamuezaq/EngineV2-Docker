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
        public int Id { get; set; }
        public string IdVendedor { get; set; }
        public string Nombre { get; set; }
        public string CodigoDeValidacion { get; set; }
        public int? Idaccount { get; set; }
        public Guid? Iduser { get; set; }
        [ForeignKey("Iduser")]
        public virtual User user { get; set; }

    }
}
