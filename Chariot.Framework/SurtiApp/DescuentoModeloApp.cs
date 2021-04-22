using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.SurtiApp {

    public class DescuentoModeloApp
    {
        public List<DescuentoDetailModeloApp> discounts { get; set; } = new List<DescuentoDetailModeloApp>();

    }

    public class DescuentoDetailModeloApp
    {
        public string name { get; set; }
        public int discount_type { get; set; } = 5;
        public bool is_percentage { get; set; }
        public double? discount_amount { get; set; }
        public double? discount_percentage { get; set; }
        public int canal_id { get; set; }
        public List<int> category_ids { get; set; } = new List<int>();
        public int id { get; set; }

    }
}
