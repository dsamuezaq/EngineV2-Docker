using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.MardisClientRestModel
{
   public class PostCoberturaClienteGuardarViewModel
    {

        public int nU_ID {set ;get;}
        public string nU_NOMBRE { set; get; }
        public Int64 nU_CEDULA_RUC { set; get; }
        public string nU_DIRECCION { set; get; }
        public string nU_TIPO_NEG { set; get; } 
        public Int64 nU_TELEFONO { set; get; }
        public Int64 nU_CODIGO_VEND { set; get; } = 176;
        public string nU_NOMBRE_VEND { set; get; } = "xxxxxx";
    }
}
