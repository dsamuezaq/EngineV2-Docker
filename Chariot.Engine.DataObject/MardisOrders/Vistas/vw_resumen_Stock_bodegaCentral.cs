﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisOrders.Vistas
{
    public class vw_resumen_Stock_bodegaCentral
    {
        public string nombre { get; set; }
        public double precio { get; set; }
        public double precioUnitario { get; set; }
        public int cantidad { get; set; }
        public string categoria { get; set; }
        public int iddistribuidor { get; set; }
        public int idproducto { get; set; }
        public int idcategoria { get; set; }

        public string barcode { get; set; }
        public string codigoProducto { get; set; }
        


    }
}
