
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisOrders
{
    [Table("Inventory_detail", Schema = "MardisOrders")]
    public class Inventory_detail
    {
        [Key]
        public int id { get; set; }
        public Nullable<int> idinventario { get; set; }
        public string codproducto { get; set; }
        public Nullable<int> valor { get; set; }
        public string unidad { get; set; }
        [ForeignKey("idinventario")]
        public virtual Inventory inventario { get; set; }
    }
}
