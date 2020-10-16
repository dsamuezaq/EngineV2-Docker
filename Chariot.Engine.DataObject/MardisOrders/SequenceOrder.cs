using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisOrders
{
    [Table("SequenceOrder", Schema = "MardisOrders")]
    public
    class SequenceOrder
    {
        [Key]
        public int Id { get; set; }
        public int Idsaleman { get; set; }

        public int Code { get; set; }

        public string estado { get; set; }

        public string uri { get; set; }
        public string imei_id { get; set; }
        public string codeunico { get; set; }
    }
}
