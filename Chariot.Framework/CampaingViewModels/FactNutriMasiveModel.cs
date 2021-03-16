using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.CampaingViewModels
{
    public class FactNutriMasiveModel
    {
        public Guid idaccount { get; set; }
        public Guid idcampaign { get; set; }
        public Guid iduser { get; set; }
        public List<FactNutriViewModel> Listbills { get; set; }
        public List<FactNutriViewModel> ListbillsErrores { get; set; } = new List<FactNutriViewModel>();
    }
}
