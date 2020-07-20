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
    [Table("Account", Schema = "MardisCommon")]
    public class Account 
    {
        public Account()
        {

            Users = new List<User>();
        }

        [Key]
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string StatusRegister { get; set; }

        //public List<Person> People { get; set; }

        public List<User> Users { get; set; }
    }
}
