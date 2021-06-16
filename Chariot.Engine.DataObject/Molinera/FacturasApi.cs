using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.Molinera
{
    [Table("FacturaAPI", Schema = "IndustrialMolinera")]
   public class FacturasApi
    {
        [Key]
        public int Id { get; set; }
        public int factura { get; set; }
        public int fecha { get; set; }
        public int codigocliente { get; set; }

        private string _codigoprod;

        public string codigoprod
        {
            get => _codigoprod.ToString().Trim();
            set => _codigoprod = value;
        }

        //  public string nombreprod { get; set; }

        private string _nombreprod;

        public string nombreprod
        {
            get => _nombreprod.ToString().Trim();
            set => _nombreprod = value;
        }


        public int cantidad { get; set; }
        public Double? precio { get; set; }
        public Double? subtotal { get; set; }
        public Double? iva { get; set; }
        public Double? total { get; set; }
        public int camion { get; set; }
        public string placa { get; set; }
        public int viaje { get; set; }
        public int codvend { get; set; }
        public string nombrevend { get; set; }
        public int pedido { get; set; }



        private string _pedidomardis;

        public string pedidomardis
        {
            get => _pedidomardis.ToString().Trim();
            set => _pedidomardis = value;
        }
        public string estadoentrega { get; set; }
        public DateTime? fechaMardis { get; set; }
    }
}
