using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.RedisViewModel
{
    public class ServiceModel
    {
        public string servicio { get; set; }
        public string descript { get; set; }

        public List<CaracteristicasModel> caract { get; set; }
    }
}
