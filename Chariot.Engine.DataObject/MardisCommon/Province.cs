using Chariot.Engine.DataObject.MardisCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisCommon
{    /// <summary>
     /// Provincias en el Sistema
     /// </summary>
    [Table("Province", Schema = "MardisCommon")]
    public class Province
    {

        public Province()
        {
            this.Districts = new HashSet<District>();
            this.Branches = new HashSet<Branch>();
        }

        [Key]
        public System.Guid Id { get; set; }
        public System.Guid IdCountry { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        [ForeignKey("IdCountry")]
        public virtual Country Country { get; set; }
        public virtual ICollection<District> Districts { get; set; }
        public virtual ICollection<Branch> Branches { get; set; }
    }
}
