using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisSecurity
{
    /// <summary>
    /// Class User Engine
    /// </summary>
    [Table("User", Schema = "MardisSecurity")]
    public class User 
    {
          
        [Key]
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string StatusRegister { get; set; }

        public Guid IdProfile { get; set; }

        public int IdPerson { get; set; }

        public Guid? Key { get; set; }

        public DateTime? DateKey { get; set; }

        public int IdAccount { get; set; }

        public string InitialPage { get; set; }


    
    }
}
