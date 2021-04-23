using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisOrders
{
    [Table("SALESMAN_DISTRIBUTOR", Schema = "MardisOrders")]
    public  class SALESMAN_DISTRIBUTOR
    {
        [Key]
        public int ID_SD { get; set; }

        public int IDSALESMAN { get; set; }
        public int IDDISTRIBUTOR { get; set; }
    }
}
