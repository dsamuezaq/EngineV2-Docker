using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisOrders
{

    
    [Table("Deposit", Schema = "MardisOrders")]
    public class Deposit
    {
        [Key]
        public int Id { get; set; }
        public string IdDeposito { get; set; }
        public string Descripcion { get; set; }
    }
}
