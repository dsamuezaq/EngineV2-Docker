using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.SurtiApp.SP
{
    public class ProductoTiendaModeloApp
    {
        public List<ProductoCatalogoTienda> catalogs { get; set; } = new List<ProductoCatalogoTienda>();
    }
    public class ProductoCatalogoTienda
    {
        public int Id { get; set; }
        public String catalog_name { get; set; }

        public List<ConsolidadoProductoBodega> products { get; set; } = new List<ConsolidadoProductoBodega>();
    }

}
