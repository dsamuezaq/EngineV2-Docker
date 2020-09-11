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
    [Table("Country", Schema = "MardisCommon")]
    public  class Country 
    {

        public Country()
        {
            this.Branches = new HashSet<Branch>();
            this.Provinces = new HashSet<Province>();
        }

        [Key]
        public System.Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string StatusRegister { get; set; }


        public virtual ICollection<Branch> Branches { get; set; }
        public virtual ICollection<Province> Provinces { get; set; }
    }
}
