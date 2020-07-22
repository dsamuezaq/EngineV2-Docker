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
    [Table("TrackingBranch", Schema = "MardisCore")]
    public  class TrackingBranch
    {
        [Key]
        public int Id {get;set;}
        public double? GeoLength { get; set; }
        public double? Geolatitude { get; set; }
        public DateTime datetime_tracking { get; set; }
        public double? timeTask { get; set; }
        public string CodeBranch { get; set; }
        public string NameBranch { get; set; }
        public string StreetBranch { get; set; }
        public string StatusBranch { get; set; }
        public string AggregateUri { get; set; }
        public string RouteBranch { get; set; }
        public int? IdPollster { get; set; }
        public int? Idcampaign { get; set; }


        [ForeignKey("Idcampaign")]
        public virtual Campaign Campaign { get; set; }
        [ForeignKey("IdPollster")]
        public virtual Pollster Pollster { get; set; }
    }
}
