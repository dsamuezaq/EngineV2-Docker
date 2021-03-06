﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.MardisOrdersViewModel
{
  public class ArticulosViewModel
    {
        public int Id { get; set; }
        public string IdArticulo { get; set; }
        public string Descripcion { get; set; }
        public string IdRubro { get; set; }
        public decimal? Iva { get; set; }
        public decimal? ImpuestosInternos { get; set; }
        public decimal? Exento { get; set; }
        public decimal? Precio1 { get; set; }
        public decimal? Precio2 { get; set; } = 0;
        public decimal? Precio3 { get; set; } = 0;
        public decimal? Precio4 { get; set; } = 0;
        public decimal? Precio5 { get; set; } = 0;
        public decimal? Precio6 { get; set; } = 0;
        public decimal? Precio7 { get; set; } = 0;
        public decimal? Precio8 { get; set; } = 0;
        public decimal? Precio9 { get; set; } = 0;
        public decimal? Precio10 { get; set; }
        public int? Idaccount { get; set; }
    }
}
