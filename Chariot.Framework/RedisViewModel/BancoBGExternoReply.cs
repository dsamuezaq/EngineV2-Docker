using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.RedisViewModel
{
  public  class BancoBGExternoReply
    {
        public string IdBG { get; set; }
        public string Nombrelocal { get; set; }
        public string Propietariolocal { get; set; }
        public string Direccionlocal { get; set; }
        public string Cedulapropietario { get; set; }
        public string tiponegocio { get; set; }
        public string provincia { get; set; }
        public string ciudad { get; set; }
        public string parroquia { get; set; }
        public string TipoEntidadBancaria { get; set; }
        public string Telefonolocal { get; set; }
        public double? latitud { get; set; }
        public double? longitud { get; set; }
        public string Estadolocal { get; set; }
        public string HorarioLunesViernes { get; set; }
        public string HorarioDomingo { get; set; }
        public string HorarioSabado { get; set; }

    }
}
