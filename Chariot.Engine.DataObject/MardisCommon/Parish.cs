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
    [Table("Parish", Schema = "MardisCommon")]
    public  class Parish { 

        public Parish()
        {
            this.Branches = new HashSet<Branch>();
        }

        [Key]
        public System.Guid Id { get; set; }
        public System.Guid IdDistrict { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string StatusRegister { get; set; }


        public virtual ICollection<Branch> Branches { get; set; }
    }
}
