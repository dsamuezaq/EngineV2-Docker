using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataObject.Helpers
{
   public class DeliveryBranches
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public string ExternalCode { get; set; }

        public string Name { get; set; }


        public string Neighborhood { get; set; }

        public string MainStreet { get; set; }

        public string SecundaryStreet { get; set; }

        public string NumberBranch { get; set; }

        public string LatitudeBranch { get; set; }

        public string LenghtBranch { get; set; }

        public string Reference { get; set; }

        public string TypeBusiness { get; set; }

        public string ClientName { get; set; }
        public string TypeDocument { get; set; } = "CI";
        public string Document { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public int camion { get; set; }

        public int factura { get; set; }
        public string estado { get; set; }
        public List<ReceivableModel> Receivables { get; set; }


    }
}
