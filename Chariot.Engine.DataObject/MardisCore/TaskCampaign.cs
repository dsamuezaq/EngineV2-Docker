using Chariot.Engine.DataObject.MardisCommon;
using Chariot.Engine.DataObject.MardisSecurity;
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



        public int IdAccount { get; set; }

        [ForeignKey("IdCampaign")]
        public Campaign Campaign { get; set; }

        public string Code { get; set; }

        public string ExternalCode { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime DateModification { get; set; } = DateTime.Now;

        public string Description { get; set; }

        public int IdBranch { get; set; }

        [ForeignKey("IdBranch")]
        public Branch Branch { get; set; } = new Branch();

        public Guid IdMerchant { get; set; }

        [ForeignKey("IdMerchant")]
        public User Merchant { get; set; }

        public string Route { get; set; }

        public string StatusRegister { get; set; } = "A";

        public int IdStatusTask { get; set; }

        public int? idPollster { get; set; }

        [ForeignKey("IdStatusTask")]
        public StatusTask StatusTask { get; set; }

        [ForeignKey("idPollster")]
        public Pollster Pollster { get; set; }
        public DateTime DateCreation { get; set; } = DateTime.Now;


        public string CommentTaskNoImplemented { get; set; }

        public Guid? UserValidator { get; set; }
        [NotMapped]
        [ForeignKey("UserValidator")]
        public User Validator { get; set; }

        public DateTime DateValidation { get; set; } = DateTime.Now;

        public string AggregateUri { get; set; }
        public string CodeGemini { get; set; }
        public string StatusMigrate { get; set; }
        public string StatusField { get; set; }
        public string Icon { get; set; }
        public string source { get; set; }
        //public ICollection<Answer> Answers { get; set; }
        //public ICollection<PollsTask> PollsTaskss { get; set; }
        //public ICollection<historialTareas> HistoryTask { get; set; }


    }
}
