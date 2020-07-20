using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisCore
{
    [Table("Task", Schema = "MardisCore")]
    public class TaskCampaign
    {
        [Key]
        public int Id { get; set; }


        public int IdCampaign { get; set; }


    }
}
