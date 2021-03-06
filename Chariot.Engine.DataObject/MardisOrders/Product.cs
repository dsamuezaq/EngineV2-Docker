﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisOrders
{
    [Table("Product", Schema = "MardisOrders")]
    public  class Product
    {
        [Key]
        public int Id { get; set; }
        public string IdArticulo { get; set; }
        public string Descripcion { get; set; }
        public string IdRubro { get; set; }
        public decimal? Iva { get; set; }
        public decimal? ImpuestosInternos { get; set; }
        public int? Exento { get; set; }
        public decimal? Precio1 { get; set; }
        public decimal? Precio2 { get; set; }
        public decimal? Precio3 { get; set; } = 0;
        public decimal? Precio4 { get; set; } = 0;
        public decimal? Precio5 { get; set; } = 0;
        public decimal? Precio6 { get; set; } = 0;
        public decimal? Precio7 { get; set; } = 0;
        public decimal? Precio8 { get; set; } = 0;
        public decimal? Precio9 { get; set; } = 0;
        public decimal? Precio10 { get; set; } = 0;
        public int? Idaccount { get; set; } = 0;
        public string StatusRegister { get; set; } = "A";
    }
}
