using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisOrders.Vistas
{
    public class vw_resumen_Stock_vendedor
    {
        public int cantidad { get; set; }
        public int id { get; set; }
        public string idVendedor { get; set; }
        public string nombre { get; set; }
        public string codigoDeValidacion { get; set; }
        public int Idaccount { get; set; }
        public int IDDISTRIBUTOR { get; set; }
        public string statusV { get; set; }
    }
}
