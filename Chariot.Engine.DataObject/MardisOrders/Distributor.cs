﻿using Chariot.Engine.DataObject.MardisSecurity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisOrders
{
    [Table("Distributor", Schema = "MardisOrders")]
    public class Distributor
    {
        [Key]
        public int IDDISTRIBUTOR { get; set; } = 0;
        public string NAME { get; set; }
        public string RUC { get; set; }
        public string ADDRESS { get; set; }
        public Guid? Iduser { get; set; }
        [ForeignKey("Iduser")]
        public virtual User user { get; set; }
        public virtual ICollection<Central_Warenhouse> Central_Warenhouses { get; set; }
    }
}
