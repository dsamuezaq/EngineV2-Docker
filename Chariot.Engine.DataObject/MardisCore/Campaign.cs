using Chariot.Engine.DataObject.MardisCommon;
//using Chariot.Framework.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisCore
{
    [Table("Campaign", Schema = "MardisCore")]
    public class Campaign 
    {
        [Key]
        public int Id { get; set; }

        public int IdAccount { get; set; } 

        public Guid IdCustomer { get; set; }

        public Guid IdStatusCampaign { get; set; }

        public Guid IdChannel { get; set; }

        public Guid IdSupervisor { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime EndDate { get; set; } = DateTime.Now;

        public DateTime RangeDate { get; set; } = DateTime.Now;

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public string Comment { get; set; }

        public string StatusRegister { get; set; } = "A";
        public string StatusAggregate { get; set; } = "A";
        public string logo { get; set; }
        public string Idform { get; set; }
        
        [ForeignKey("IdAccount")]
        public virtual Account Account { get; set; }

    }
}
