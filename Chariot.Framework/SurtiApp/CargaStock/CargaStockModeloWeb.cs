using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.SurtiApp.CargaStock
{
   public class CargaStockModeloWeb
    {
        public int account { get; set; }
        public string iduser { get; set; }
        public int option { get; set; }
        public CargaStockItemModelWeb stockCamion { get; set; }
    }
}
