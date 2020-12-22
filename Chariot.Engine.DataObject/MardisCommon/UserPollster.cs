using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisCommon
{
    [Table("UserPollster" , Schema = "MardisCommon")]
    public class UserPollster
    {
     [Key]
    public int Id { get; set; }
    public Guid IdUser {get;set;}
    public int IdPollster {get;set;}
    public int IdAccount {get;set;}

    }
}
