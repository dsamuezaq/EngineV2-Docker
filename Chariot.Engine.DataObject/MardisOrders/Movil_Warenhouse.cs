using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisOrders
{
    [Table("MOVIL_WAREHOUSE", Schema = "MardisOrders")]
    public class Movil_Warenhouse
    {
        [Key]
        public Int64 ID_MOVILW { get; set; } = 0;
        public decimal? BALANCE { get; set; }
        public string DESCRIPTION { get; set; }
        public int IDVENDEDOR { get; set; }
        public string MOVEMENT { get; set; }
        public int IDPRODUCTO { get; set; }
        public string COMMENT { get; set; }

        [ForeignKey("IDPRODUCTO")]
        public virtual Product product { get; set; }
    }
}
