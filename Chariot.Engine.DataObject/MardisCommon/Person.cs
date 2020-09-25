using Chariot.Engine.DataObject.MardisCore;
using Chariot.Engine.DataObject.MardisSecurity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisCommon
{
    [Table("Person", Schema = "MardisCommon")]
    public class Person 
    {

        public Person()
        {
            Users = new HashSet<User>();
      //      Branches = new HashSet<Branch>();
        }

        [Key]
        public int Id { get; set; } = 0;
        public int IdAccount { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string TypeDocument { get; set; } = "CI";
        public string Document { get; set; }
        public string StatusRegister { get; set; } = "A";
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string mail { get; set; }



        [ForeignKey("IdAccount")]
        public virtual Account Account { get; set; }
       // public ICollection<Branch> Branches { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
