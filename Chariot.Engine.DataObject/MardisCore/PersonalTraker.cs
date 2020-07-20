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
    [Table("Tracking", Schema = "MardisCore")]
    public class PersonalTraker
    {
        [Key]
        public int Id { get; set; }

        public double GeoLength { set; get; }
        public double Geolatitude { set; get; }
        public double GeoAccuracy { set; get; }
        public DateTime LastDate { set; get; }
        public int Idcampaign { set; get; }
        public string NameAccount { set; get; }
        public string Namecampaign { set; get; }
        public int IdPollster { set; get; }

        [ForeignKey("Idcampaign")]
        public virtual Campaign Campaign { get; set; }
        [ForeignKey("IdPollster")]
       public virtual Pollster Pollster   { get; set;  } 
        
        }
}
