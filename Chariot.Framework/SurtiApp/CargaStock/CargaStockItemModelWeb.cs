using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.SurtiApp.CargaStock
{
  public  class CargaStockItemModelWeb
    {
        public string Id_Producto { get; set; }
        public string Nombre_Producto { get; set; }
        public string Vendedor { get; set; }
        public string Cedula { get; set; }
        public string Cantidad { get; set; }
        public string Errores { get; set; }
    }
}
