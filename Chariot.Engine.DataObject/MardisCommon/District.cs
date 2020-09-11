using Chariot.Engine.DataObject.MardisCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Chariot.Engine.DataObject.MardisCommon
{
    [Table("District", Schema = "MardisCommon")]
    public  class District
    {

        public District()
        {
            this.Branches = new HashSet<Branch>();
            this.Sectors = new HashSet<Sector>();
        }

        [Key]
        public System.Guid Id { get; set; }
        public System.Guid IdProvince { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string StatusRegister { get; set; }

        public long? IdManagement { get; set; }


        public virtual ICollection<Branch> Branches { get; set; }
        [ForeignKey("IdProvince")]
        public virtual Province Province { get; set; }
        public virtual ICollection<Sector> Sectors { get; set; }
    }
}
