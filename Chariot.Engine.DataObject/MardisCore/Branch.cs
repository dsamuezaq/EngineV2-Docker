using Chariot.Engine.DataObject.MardisCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisCore
{
    /// <summary>
    /// Tabla de Locales en el sistema
    /// </summary>
    [Table("Branch", Schema = "MardisCore")]
    public class Branch 
    {
        [Key]
        public int Id { get; set; } =0;

        public int IdAccount { get; set; } = 0;

        public string Code { get; set; }

        public string ExternalCode { get; set; }

        public string Name { get; set; }

        public string Label { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public Guid IdCountry { get; set; } = Guid.Empty;

        public Guid IdProvince { get; set; } = Guid.Empty;

        public Guid IdDistrict { get; set; } = Guid.Empty;

        public Guid IdParish { get; set; } = Guid.Empty;

        public Guid IdSector { get; set; } = Guid.Empty;

        public string Zone { get; set; }

        public string Neighborhood { get; set; }

        public string MainStreet { get; set; }

        public string SecundaryStreet { get; set; }

        public string NumberBranch { get; set; }

        public string LatitudeBranch { get; set; }

        public string LenghtBranch { get; set; }

        public string Reference { get; set; }

        public int IdPersonOwner { get; set; } = 0;

        public string IsAdministratorOwner { get; set; }

    

        public string StatusRegister { get; set; } ="A";

        public string TypeBusiness { get; set; }
        public string ESTADOAGGREGATE { get; set; }
        public string RUTAAGGREGATE { get; set; }
        public string IMEI_ID { get; set; }
        public string CommentBranch { get; set; }

        public string Cluster { get; set; }
        public string state_period { get; set; }
        public int? geoupdate { get; set; }
        

        //public string Ext_image { get; set; }

        //public string Isclient { get; set; }

        //public Double? TimeLastTask { get; set; }
        public DateTime? routeDate { get; set; } = DateTime.Now;
        [ForeignKey("IdCountry")]
        public Country Country { get; set; }

        [ForeignKey("IdDistrict")]
        public District District { get; set; }

        [ForeignKey("IdParish")]
        public Parish Parish { get; set; }
        [ForeignKey("IdProvince")]
        public Province Province { get; set; }
        [ForeignKey("IdSector")]
        public Sector Sector { get; set; }
        [ForeignKey("IdPersonOwner")]
        public virtual Person PersonOwner { get; set; } = new Person();

    }
}
