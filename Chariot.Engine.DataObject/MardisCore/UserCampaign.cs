using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisCore
{
    [Table("UserCanpaign", Schema = "MardisCore")]
    public class UserCampaign
    {
        [Key]
        public Guid id { get; set; }
        public Guid idUser { get; set; }
        public string status { get; set; }
        public int idCanpaign { get; set; }
    }
}
