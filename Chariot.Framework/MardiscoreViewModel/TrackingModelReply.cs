﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.MardiscoreViewModel
{
   public class TrackingModelReply
    {
        public int Idpollster { set; get; }
        public string first_name { set; get; }
        public string last_name { set; get; }
        public double latitud { set; get; }
        public double longitud { set; get; }
        public int bateria { set; get; }
        public string estado { set; get; }
        public DateTime Ultima_conexion { set; get; }

        public DateTime? Inicio { set; get; }
        public DateTime? Fin { set; get; }

        public string Telefono { set; get; }

    }
}
