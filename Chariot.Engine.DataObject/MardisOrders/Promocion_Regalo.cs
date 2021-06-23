using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisOrders
{
    [Table("Promocion_Regalo", Schema = "MardisOrders")]
    public  class Promocion_Regalo
    {
        [Key]
        public int Id { get; set; }
        public int? Id_prom { get; set; }
        public int? Id_pro { get; set; }
        public int? Pro_art_cantidad { get; set; }         
        public DateTime? Fecha_creacion { get; set; }        
    }
}
