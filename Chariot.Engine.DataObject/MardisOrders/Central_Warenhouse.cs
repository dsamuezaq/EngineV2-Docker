using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisOrders
{
    [Table("CENTRAL_WAREHOUSE", Schema = "MardisOrders")]
    public class Central_Warenhouse
    {
        [Key]
        public Int64 ID_CENTRALW { get; set; } = 0;
        public int IDPRODUCTO { get; set; }
        [ForeignKey("IDPRODUCTO")]
        public virtual Product product { get; set; }
        //[ForeignKey("ID_MOVILW")]
        //public int ID_MOVILW { get; set; }
        public int IDDISTRIBUTOR { get; set; }
        [ForeignKey("IDDISTRIBUTOR")]
        public virtual Distributor Distributor { get; set; }
        public decimal BALANCE { get; set; }
        public string DESCRIPTION { get; set; }
        public string MOVEMENT { get; set; }


    }
}
