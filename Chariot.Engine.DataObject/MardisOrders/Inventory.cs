using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisOrders
{
    [Table("Inventory", Schema = "MardisOrders")]
    public class Inventory
    {
        [Key]
        public int id { get; set; }
        public string codcliente { get; set; }
        public string codvendedor { get; set; }
        public Nullable<System.DateTime> fechainventario { get; set; }

     
        public virtual ICollection<Inventory_detail> inventariodetalles { get; set; }
    }
}
