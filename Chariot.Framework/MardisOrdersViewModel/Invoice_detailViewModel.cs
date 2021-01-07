using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.MardisOrdersViewModel
{
    public class Invoice_detailViewModel
    {
        public string codigoprod { get; set; }
        public string nombreprod { get; set; }
        public int cantidad { get; set; }

        public       Double? precio { get; set; }
}
}
