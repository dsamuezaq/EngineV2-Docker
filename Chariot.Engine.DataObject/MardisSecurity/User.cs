﻿using Chariot.Engine.DataObject.MardisCommon;
using Chariot.Engine.DataObject.MardisOrders;
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


        public User()
        {
            this.Salesmanes = new HashSet<Salesman>();
        }
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
        [ForeignKey("IdPerson")]
        public virtual Person Persons { get; set; }
        [ForeignKey("IdProfile")]
        public virtual Profile Profiles { get; set; }
        public string InitialPage { get; set; }

        [ForeignKey("IdAccount")]
        public virtual Account Account { get; set; }

    public virtual ICollection<Salesman> Salesmanes { get; set; }


}
}
