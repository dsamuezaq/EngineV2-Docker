using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisOrders
{
    [Table("Promocion", Schema = "MardisOrders")]
    public  class Promocion
    {
        [Key]
        public int Id { get; set; }
        public string Pro_nombre { get; set; }
        public int? Pro_efecto_promo { get; set; }
        public int? Pro_iddistribuidor { get; set; }
        public string Pro_tipo { get; set; }
        public string Pro_estado { get; set; }        
    }
}
