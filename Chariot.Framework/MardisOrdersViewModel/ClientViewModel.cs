using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.MardisOrdersViewModel
{
   public class ClientViewModel
    {
        public int Id { get; set; } = 0;
        public string Codigo { get; set; }
        public string CodigoOpcional { get; set; }
        public string RazonSocial { get; set; }
        public string CalleNroPisoDpto { get; set; }
        public string Localidad { get; set; }
        public string Cuit { get; set; }
        public decimal? Iva { get; set; }
        public decimal? ClaseDePrecio { get; set; }
        public decimal? PorcDto { get; set; }
        public string CpteDefault { get; set; }
        public string IdVendedor { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }
}
