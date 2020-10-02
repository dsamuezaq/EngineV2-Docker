using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.MardisOrdersViewModel
{
    public class ExcelProductViewModelReply
    {
        public string Codigo {get;set; }
        public string Sku { get; set; }
        public string IVA { get; set; } 
        public string Impuesto_interno { get; set; }
        public string Exento { get; set; }
        public string Cantidad { get; set; }
        public string Precio { get; set; }
        public int Idaccount { get; set; }
        public string Error { get; set; }

    }
}
