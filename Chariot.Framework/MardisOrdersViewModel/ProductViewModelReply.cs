using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.MardisOrdersViewModel
{
    public class ProductViewModelReply
    {
        public string Codigo {get;set; }
        public string Sku { get; set; }
        public string IVA { get; set; } 
        public string Impuesto_interno { get; set; }
        public string Exento { get; set; }
        public Decimal? Cantidad { get; set; }
        public Decimal? Precio { get; set; }

    }
}
