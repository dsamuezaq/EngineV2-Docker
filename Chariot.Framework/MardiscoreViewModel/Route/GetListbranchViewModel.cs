using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.MardiscoreViewModel.Route
{
    public class GetListbranchViewModel
    {
        public int account { get; set; }
        public string iduser { get; set; }
        public int option { get; set; }
        public int campaign { get; set; }
        public int status { get; set; }

        public ListBranchExcelModel _route { get; set; }
    }
}
