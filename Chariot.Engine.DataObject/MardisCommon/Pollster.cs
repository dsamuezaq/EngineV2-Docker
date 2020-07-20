using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisCommon
{
    [Table("pollster", Schema = "MardisCommon")]
    public class Pollster
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string IMEI { get; set; }
        public string Phone { get; set; }
        public string Qsupport { get; set; }
        public DateTime? Fecha_Inicio { get; set; } = DateTime.Now;

        public DateTime? Fecha_Fin { get; set; } = DateTime.Now;
        public string Status { get; set; }

        public string Oficina { get; set; }
        public string UserCel { get; set; }
        public string PassCel { get; set; }

        public int? idaccount { get; set; }
    }
}
