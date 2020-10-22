using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.MardisOrdersViewModel
{
    public class InvoiceViewModel
    {
        public int factura { get; set; }
        public int fecha { get; set; }
        public Double? precio { get; set; }
        public Double? subtotal { get; set; }
        public Double? iva { get; set; }
        public Double? total { get; set; }
       // public int camion { get; set; }
       // public string placa { get; set; }
        //public int viaje { get; set; }
        public int codvend { get; set; }
        public string nombrevend { get; set; }
        public List<Invoice_detailViewModel> Invoice_details { get; set; }

    }
}
