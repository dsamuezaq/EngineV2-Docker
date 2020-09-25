using Chariot.Engine.DataObject.MardisCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisCore
{
    [Table("statusTaskAccount", Schema = "MardisCore")]
    public class StatusTaskAccount 
    {
        public StatusTaskAccount()
        {

        }

        [Key]
        public int id { get; set; }

        public int Idaccount { get; set; } = 0;

        public int Idstatustask { get; set; }

        public int ORDER { get; set; }


        [ForeignKey("Idaccount")]
        public virtual Account Cuenta { get; set; }

        [ForeignKey("Idstatustask")]
        public StatusTask StatusTasks { get; set; }
    }
}
