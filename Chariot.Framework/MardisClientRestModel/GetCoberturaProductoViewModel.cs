using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.MardisClientRestModel
{
    public class GetCoberturaProductoViewModel
    {
        public string codigoprod { get; set; }
        public string nombreprod { get; set; }
        public string linea { get; set; }
        public decimal? precioprod { get; set; }
        public string pagaiva { get; set; }
        public decimal? stock { get; set; }
    }
}
