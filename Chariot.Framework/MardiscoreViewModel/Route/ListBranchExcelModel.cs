using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.MardiscoreViewModel.Route
{
    public class ListBranchExcelModel
    {

        [Required]

        public string Codigo_Encuesta { get; set; }
        public string PT_indice { get; set; }
        public string Tipo { get; set; }
        public string local { get; set; }
        public string Dirección { get; set; }
        public string Referencia { get; set; }

        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Mail { get; set; }
        public string Cédula { get; set; }
        public string Celular { get; set; }
        public string Telefono { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Parroquia { get; set; }
        public string CLUSTER { get; set; }
        public string Estado { get; set; }
        public string RUTA { get; set; }
        public string IMEI { get; set; }

        public String Fecha { get; set; }
        public String Errores { get; set; }

    }
}
