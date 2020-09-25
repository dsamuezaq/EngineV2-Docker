using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisCore
{

    [Table("StatusTask", Schema = "MardisCore")]
    public class StatusTask
    {

        public StatusTask()
        {
            Tasks = new HashSet<TaskCampaign>();
        }
        [Key]

        public int Id { get; set; } = 0;
        public string Code { get; set; }
        public string Name { get; set; }
        public string StatusRegister { get; set; }
        public string color { get; set; }
        public ICollection<TaskCampaign> Tasks { get; set; }
    }
}
