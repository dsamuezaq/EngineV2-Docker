using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.MardisOrdersViewModel
{
    public    class VisitasViewModel
    {
        public int Id { get; set; } = 0;
        public string codcliente { get; set; }
        public string codvendedor { get; set; }
        public string fechavisita { get; set; }
        public float Latitud { get; set; }
        public float Longitud { get; set; }
        public string Linkfotoexterior { get; set; }
        public string Compro { get; set; }
        public string Observacion { get; set; }
        public string estado { get; set; }

        public int? idaccount { get; set; } = 15;
    }
}
