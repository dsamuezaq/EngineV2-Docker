using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.MardiscoreViewModel.Branch
{
    public class BranchRutaTaskViewModel
    {
        public int Id { get; set; }
        public int IdAccount { get; set; }
        public string ExternalCode { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string MainStreet { get; set; }
        public string Neighborhood { get; set; }
        public string Reference { get; set; }
        public string Propietario { get; set; }
        public Guid IdProvince { get; set; }
        public Guid IdDistrict { get; set; }
        public Guid IdParish { get; set; }
        public string RUTAAGGREGATE { get; set; }
        public string IMEI_ID { get; set; }
        public string LatitudeBranch { get; set; }
        public string LenghtBranch { get; set; }
        public string Celular { get; set; }
        public string TypeBusiness { get; set; }
        public string Cedula { get; set; }
        public string ZonaPeliograsa { get; set; }
        public int? ActualizoGeo { get; set; }
        public string ESTADOAGGREGATE { get; set; }
        public string comment { get; set; }
        public List<int> actividad { get; set; }
        public string Province { get; set; }
        public string District { get; set; }

        public Double? TimeLastTask { get; set; }
        public DateTime? FechaVisita { get; set; }
        public DateTime? FechaVisitaInicio { get; set; }
        public string Link { get; set; }
        public string isclient { get; set; }
        public string Propietarioape { get; set; }
        public string correo { get; set; }
        //public List<ViewContact> Contacts { get; set; }
    }
}
