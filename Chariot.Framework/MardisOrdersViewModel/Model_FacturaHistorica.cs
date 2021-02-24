using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.MardisOrdersViewModel
{
   public class Model_FacturaHistorica
    {

        public int NumeroFactura { get; set; }
        public double TotalFactura { get; set; }
        public double TotalDevolucion { get; set; }
        public List<Model_DellateFactura> detalleFacturas { get; set; }
        public List<Modelo_DetalleDevolucion> detalleDevoluciones { get; set; }
        //  public double TotalCartera { get; set; }
    }

}
