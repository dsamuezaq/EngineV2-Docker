using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisOrders
{

    [Table("vw_pagoxcarteraDevolucion", Schema = "MardisOrders")]
   public class vw_pagoxcarteraDevolucion
    {
        [Key]
        public Int64 Id { get; set; }
        public double? cO_CODCLI { get; set; }
        public double? valor { get; set; }
    }
}
