using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.SurtiApp.SP
{
    public class ItemCarritoModelSurtiApp
    {
        public string delivery_date { get; set; }
        public List<ItemCarritoDetalleModelSurtiApp> shopping_carts { get; set; } = new List<ItemCarritoDetalleModelSurtiApp>();


    }

    public class ItemCarritoDetalleModelSurtiApp
    {

        public List<int> product_attributes = new List<int>();
        public int quantity { get; set; }
        public double? weight { get; set; } 
        public DateTime created_on_utc { get; set; }
        public DateTime updated_on_utc { get; set; }
        public string shopping_cart_type { get; set; }
        public int product_id { get; set; }
        public ConsolidadoProductoBodega product { get; set; }
        public int  customer_id { get; set; }
        public int id { get; set; }

    }

   
}
