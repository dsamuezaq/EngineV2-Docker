using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisOrders
{
    [Table("Promocion_articulos", Schema = "MardisOrders")]
    public  class Promocion_articulos
    {
        [Key]
        public int Id { get; set; }
        public int Id_prom { get; set; } = 0;
        public int Id_pro { get; set; } = 0;
        public int? Pro_art_cantidad { get; set; }
        public int? Pro_art_apl_descuento { get; set; }   
        public DateTime? Fecha_creacion { get; set; }        
    }
}
