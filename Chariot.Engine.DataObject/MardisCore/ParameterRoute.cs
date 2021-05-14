using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.MardisCore
{    /// <summary>
     /// Tabla de Locales en el sistema
     /// </summary>
    [Table("ParameterRoute", Schema = "MardisCore")]
    public class ParameterRoute
    {
        [Key]
        public int id { get; set; }
        public string codigoRuta { get; set; }
        public string latitud
        { get; set; }
        public string longitud { get; set; }
        public string personaAsignada
        { get; set; }
        public DateTime? fechaCreacion { get; set; }
        public DateTime? fechaEjecucion
        { get; set; }
        public int idaccount { get; set; }
        public string estado
        { get; set; }
    }
}
