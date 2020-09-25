using Chariot.Engine.DataObject.MardisOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.MardisOrdersViewModel
{
    public class InventaryViewModel
    {
        public int id { get; set; }
        public string codcliente { get; set; }
        public string codvendedor { get; set; }
        public Nullable<System.DateTime> fechainventario { get; set; }


        public virtual ICollection<Inventory_detail> inventariodetalles { get; set; }
    }
}
