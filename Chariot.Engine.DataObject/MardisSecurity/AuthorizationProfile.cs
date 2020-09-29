using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisSecurity
{
    [Table("AuthorizationProfile", Schema = "MardisSecurity")]
    public class AuthorizationProfile
    {

        [Key]
        public System.Guid Id { get; set; }

        public System.Guid IdProfile { get; set; }

        public System.Guid IdMenu { get; set; }

        [ForeignKey("IdProfile")]
        public virtual Profile Profile { get; set; }

        [ForeignKey("IdMenu")]
        public virtual Menu Menu { get; set; }

    }
}