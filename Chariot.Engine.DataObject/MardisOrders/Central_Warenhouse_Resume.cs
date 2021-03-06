﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisOrders
{
    [Table("CENTRAL_WAREHOUSE_RESUME", Schema = "MardisOrders")]
    public class Central_Warenhouse_Resume
    {
        [Key]
        public Int64 ID { get; set; } = 0;
        public decimal? BALANCE { get; set; }
        public int IDDISTRIBUTOR { get; set; }
        public int IDPRODUCTO { get; set; }
        //[ForeignKey("IDPRODUCTO")]
        //public virtual Product product { get; set; }
    }
}
