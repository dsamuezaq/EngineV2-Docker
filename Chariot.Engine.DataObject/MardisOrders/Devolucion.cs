using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisOrders
{
    [Table("Devolucion", Schema = "MardisOrders")]
    public  class Devolucion
    {
        [Key]
        public int Id { get; set; }
        public int p_ORDENDEV { get; set; }
        public string p_FECHA { get; set; }
        public string p_PRODUCTO { get; set; }
        public float p_CANTIDAD { get; set; }
        public string p_CLIENTE { get; set; }
        public string p_VENDEDOR { get; set; }
        public string p_unidad { get; set; }


    }
}
